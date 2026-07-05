using PruebaConfiamed.Entities;

namespace PruebaConfiamed.Interfaces
{
    public interface IUserRepository
    {
        Task<List<AppUser>> GetAllAsync();

        Task<AppUser?> GetByIdAsync(int id);

        Task<AppUser?> GetByUsernameAsync(string username);

        Task AddAsync(AppUser user);

        Task SaveChangesAsync();

        // Consultas específicas del negocio

        Task<List<AppUser>> GetUsersOrderedByPendingItemsAsync();

        Task<int> GetPendingItemsCountAsync(int userId);

        Task<int> GetCompletedItemsCountAsync(int userId);

        Task<int> GetHighPriorityPendingItemsCountAsync(int userId);

        Task<List<AppUser>> GetUsersOrderedByTotalItemsAsync();
    }
}