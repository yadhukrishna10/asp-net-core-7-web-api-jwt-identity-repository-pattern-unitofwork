using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
    public interface IUserService
    {
        User getById(Guid userId);
		UserDto login(string email, string password);
		Task<User> register(UserDto user);
        Task<User> UpdateAsync(User user);
        List<User> getAllUsers();
		string GetUserId();
		UserProfileDto GetCurrentUser();
		 string GetCurrentUsername();
		 string GetCurrentUserRole();
		string GetCurrentUserEmail();

	}
}
