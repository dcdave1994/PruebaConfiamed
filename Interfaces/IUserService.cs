using PruebaConfiamed.DTOs.Requests;
using PruebaConfiamed.DTOs.Responses;

public interface IUserService
{
    Task<UserResponse> CreateAsync(CreateUserRequest request);

    Task<List<UserResponse>> GetAllAsync();

    Task<UserResponse?> GetByIdAsync(int id);
}