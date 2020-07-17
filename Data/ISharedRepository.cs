using System.Threading.Tasks;

namespace TalentHunt.Data
{
    public interface ISharedRepository
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveAll();
        void Delete<T>(T entity) where T : class;
    }
}