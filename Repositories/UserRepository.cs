using Microsoft.EntityFrameworkCore;
using PruebaConfiamed.Common.Enums;
using PruebaConfiamed.Data;
using PruebaConfiamed.Entities;
using PruebaConfiamed.Interfaces;

namespace PruebaConfiamed.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task<AppUser?> GetByIdAsync(int id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public async Task<AppUser?> GetByUsernameAsync(string username)
        {
            return await _context.AppUsers
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task AddAsync(AppUser user)
        {
            await _context.AppUsers.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetPendingItemsCountAsync(int userId)
        {
            return await _context.WorkItems.CountAsync(x =>
                x.AppUserId == userId &&
                x.Status == WorkItemStatus.Pending);
        }

        public async Task<int> GetCompletedItemsCountAsync(int userId)
        {
            return await _context.WorkItems.CountAsync(x =>
                x.AppUserId == userId &&
                x.Status == WorkItemStatus.Completed);
        }

        public async Task<int> GetHighPriorityPendingItemsCountAsync(int userId)
        {
            return await _context.WorkItems.CountAsync(x =>
                x.AppUserId == userId &&
                x.Status == WorkItemStatus.Pending &&
                x.Priority == Priority.High);
        }

        public async Task<List<AppUser>> GetUsersOrderedByPendingItemsAsync()
        {
            return await _context.AppUsers
                .OrderBy(u => _context.WorkItems.Count(w =>
                    w.AppUserId == u.Id &&
                    w.Status == WorkItemStatus.Pending))
                .ToListAsync();
        }

        public async Task<List<AppUser>> GetUsersOrderedByTotalItemsAsync()
        {
            return await _context.AppUsers
                .OrderBy(u => _context.WorkItems.Count(w =>
                    w.AppUserId == u.Id))
                .ToListAsync();
        }
    }
}