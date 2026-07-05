using PruebaConfiamed.DTOs.Requests;
using PruebaConfiamed.DTOs.Responses;
using PruebaConfiamed.Entities;
using PruebaConfiamed.Interfaces;

namespace PruebaConfiamed.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponse> CreateAsync(CreateUserRequest request)
        {
            var existing = await _repository.GetByUsernameAsync(request.Username);

            if (existing != null)
                throw new Exception("El usuario ya existe.");

            var user = new AppUser
            {
                Username = request.Username
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                PendingItems = 0,
                CompletedItems = 0,
                HighPriorityItems = 0
            };
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            var response = new List<UserResponse>();

            foreach (var user in users)
            {
                response.Add(new UserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    PendingItems = await _repository.GetPendingItemsCountAsync(user.Id),
                    CompletedItems = await _repository.GetCompletedItemsCountAsync(user.Id),
                    HighPriorityItems = await _repository.GetHighPriorityPendingItemsCountAsync(user.Id)
                });
            }

            return response;
        }

        public async Task<UserResponse?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserResponse
            {
                Id = user.Id,
                Username = user.Username,
                PendingItems = await _repository.GetPendingItemsCountAsync(user.Id),
                CompletedItems = await _repository.GetCompletedItemsCountAsync(user.Id),
                HighPriorityItems = await _repository.GetHighPriorityPendingItemsCountAsync(user.Id)
            };
        }
    }
}