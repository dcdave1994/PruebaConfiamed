using PruebaConfiamed.Entities;

namespace PruebaConfiamed.Interfaces
{
    public interface IWorkItemRepository
    {
        Task<List<WorkItem>> GetAllAsync();

        Task<WorkItem?> GetByIdAsync(int id);

        Task AddAsync(WorkItem workItem);

        Task SaveChangesAsync();

        // Consultas específicas

        Task<List<WorkItem>> GetPendingByUserAsync(int userId);

        Task<List<WorkItem>> GetCompletedByUserAsync(int userId);

        Task<List<WorkItem>> GetPendingAsync();

        Task<List<WorkItem>> GetHighPriorityPendingAsync();
    }
}