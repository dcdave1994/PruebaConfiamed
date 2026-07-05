using Microsoft.EntityFrameworkCore;
using PruebaConfiamed.Common.Enums;
using PruebaConfiamed.Data;
using PruebaConfiamed.Entities;
using PruebaConfiamed.Interfaces;

namespace PruebaConfiamed.Repositories
{
    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkItem>> GetAllAsync()
        {
            return await _context.WorkItems
                .Include(w => w.AppUser)
                .ToListAsync();
        }

        public async Task<WorkItem?> GetByIdAsync(int id)
        {
            return await _context.WorkItems
                .Include(w => w.AppUser)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task AddAsync(WorkItem workItem)
        {
            await _context.WorkItems.AddAsync(workItem);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<WorkItem>> GetPendingByUserAsync(int userId)
        {
            return await _context.WorkItems
                .Where(x => x.AppUserId == userId &&
                            x.Status == WorkItemStatus.Pending)
                .OrderByDescending(x => x.Priority)
                .ThenBy(x => x.DueDate)
                .ToListAsync();
        }

        public async Task<List<WorkItem>> GetCompletedByUserAsync(int userId)
        {
            return await _context.WorkItems
                .Where(x => x.AppUserId == userId &&
                            x.Status == WorkItemStatus.Completed)
                .ToListAsync();
        }

        public async Task<List<WorkItem>> GetPendingAsync()
        {
            return await _context.WorkItems
                .Where(x => x.Status == WorkItemStatus.Pending)
                .Include(x => x.AppUser)
                .ToListAsync();
        }

        public async Task<List<WorkItem>> GetHighPriorityPendingAsync()
        {
            return await _context.WorkItems
                .Where(x => x.Status == WorkItemStatus.Pending &&
                            x.Priority == Priority.High)
                .Include(x => x.AppUser)
                .ToListAsync();
        }
    }
}