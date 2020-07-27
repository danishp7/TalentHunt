using System.Collections.Generic;
using System.Threading.Tasks;
using TalentHunt.Dtos;
using TalentHunt.Models;

namespace TalentHunt.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExist(string userName);
        bool IsPassword(string password, byte[] passwordHash, byte[] passwordSalt);
        List<byte[]> CreatePasswordWithEncryption(string password);
    }
}