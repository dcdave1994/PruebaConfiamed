using PruebaConfiamed.Common.Enums;
using PruebaConfiamed.DTOs.Requests;
using PruebaConfiamed.Entities;
using PruebaConfiamed.Interfaces;

namespace PruebaConfiamed.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IWorkItemRepository _workItemRepository;
        private readonly IUserRepository _userRepository;

        public WorkItemService(
            IWorkItemRepository workItemRepository,
            IUserRepository userRepository)
        {
            _workItemRepository = workItemRepository;
            _userRepository = userRepository;
        }

        private bool IsUrgent(DateTime dueDate)
        {
            return dueDate <= DateTime.Now.AddDays(3);
        }

        private async Task<AppUser?> SelectUserAsync(CreateWorkItemRequest request)
        {
            // Regla de saturación:
            // Si un usuario tiene más de 3 ítems de prioridad alta pendientes (aqui incluyo el 3),
            // no se toma en cuenta para la distribución.
            async Task<bool> IsUserAvailable(AppUser user)
            {
                var highPriorityPendingCount =
                    await _userRepository.GetHighPriorityPendingItemsCountAsync(user.Id);

                return highPriorityPendingCount <= 3;
            }

            // Regla 1:
            // Si la fecha está próxima a vencer, se asigna al usuario con menos ítems totales.
            if (IsUrgent(request.DueDate))
            {
                var usersOrderedByTotalItems =
                    await _userRepository.GetUsersOrderedByTotalItemsAsync();

                foreach (var user in usersOrderedByTotalItems)
                {
                    if (await IsUserAvailable(user))
                        return user;
                }

                return null;
            }

            // Regla 2:
            // Para cualquier prioridad, se toma al usuario con menor lista de pendientes,
            // excluyendo usuarios saturados.
            var usersOrderedByPendingItems =
                await _userRepository.GetUsersOrderedByPendingItemsAsync();

            foreach (var user in usersOrderedByPendingItems)
            {
                if (await IsUserAvailable(user))
                    return user;
            }

            return null;
        }
        public async Task<List<WorkItem>> GetAllAsync()
        {
            return await _workItemRepository.GetAllAsync();
        }

        public async Task<WorkItem?> GetByIdAsync(int id)
        {
            return await _workItemRepository.GetByIdAsync(id);
        }

        public async Task<WorkItem> CreateAsync(CreateWorkItemRequest request)
        {
            var selectedUser = await SelectUserAsync(request);

            if (selectedUser == null)
                throw new Exception("No existe un usuario disponible para asignar el WorkItem.");

            var workItem = new WorkItem
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                Priority = request.Priority,
                Status = WorkItemStatus.Pending,
                AppUserId = selectedUser.Id
            };

            await _workItemRepository.AddAsync(workItem);
            await _workItemRepository.SaveChangesAsync();

            return workItem;
        }
    }
}