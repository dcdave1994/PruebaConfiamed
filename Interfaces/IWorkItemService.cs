using PruebaConfiamed.DTOs;
using PruebaConfiamed.DTOs.Requests;
using PruebaConfiamed.Entities;

namespace PruebaConfiamed.Interfaces
{
    public interface IWorkItemService
    {
        Task<WorkItem> CreateAsync(CreateWorkItemRequest request);

        Task<List<WorkItem>> GetAllAsync();

        Task<WorkItem?> GetByIdAsync(int id);
    }
}