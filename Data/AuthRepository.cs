using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TalentHunt.Models;

namespace TalentHunt.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly TalentHuntContext _ctx;
        private readonly ISharedRepository _sharedRepo;
        private readonly Random _random = new Random();
        public AuthRepository(TalentHuntContext context, ISharedRepository sharedRepository)
        {
            _ctx = context;
            _sharedRepo = sharedRepository;
        }

        public async Task<User> Login(string userName, string password)
        {
            userName = userName.ToLower();
            var user = await _ctx.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
                return null;

            var isPassword = VerifyPassword(password, user.HashPassword, user.PasswordSalt);
            if (isPassword == false)
                return null;
            
            return user;

        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hashedPasswordFromDb = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var enteredPasswordHash = hashedPasswordFromDb.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < enteredPasswordHash.Length; i++)
                {
                    if (passwordHash[i] != enteredPasswordHash[i])
                        return false;
                }
            }
            return true;
        }

        public bool IsPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hashedPasswordFromDb = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var enteredPasswordHash = hashedPasswordFromDb.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < enteredPasswordHash.Length; i++)
                {
                    if (passwordHash[i] != enteredPasswordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            user.UserName = user.UserName.ToLower();
            CreatePasswordWithEncryption(password, out byte[] passwordHash, out byte[] key);
            user.HashPassword = passwordHash;
            user.PasswordSalt = key;
            user.NormalizedUserName = user.UserName.ToUpper().Normalize();
            user.NormalizedEmail = user.Email.ToUpper().Normalize();
            user.PasswordHash = password;
            _sharedRepo.Add<User>(user);
            
            if (await _sharedRepo.SaveAll())
                return user;

            return null;
        }

        private void CreatePasswordWithEncryption(string password, out byte[] passwordHash, out byte[] key)
        {
            using (var hashedPassword = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hashedPassword.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                key = hashedPassword.Key;
            }
        }

        public List<byte[]> CreatePasswordWithEncryption(string password)
        {
            byte[] passwordHash, key;
            using (var hashedPassword = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hashedPassword.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                key = hashedPassword.Key;
            }
            List<byte[]> password_hash_key = new List<byte[]>();
            password_hash_key.Add(passwordHash);
            password_hash_key.Add(key);
            return password_hash_key;
        }

        public async Task<bool> UserExist(string userName)
        {
            var user = await _ctx.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
                return false;
            return true;
        }
    }
}
