using MovieWebApi.presentation.dto;

namespace MovieWebApi.service.interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(UserDTO userDTO);
        Task<LoginResponseDTO> LoginAsync(UserDTO userDTO);
        Task<List<UserDTO>> FindAllAsync();
        Task<UserDTO?> FindByIdAsync(long id);
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task<UserDTO?> UpdateUserAsync(UserDTO userDTO, long id);
        Task<UserDTO?> DeleteUserAsync(long id);
    }
}
