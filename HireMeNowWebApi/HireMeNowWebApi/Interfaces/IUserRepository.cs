using HireMeNowWebApi.Models;

namespace HireMeNowWebApi.Interfaces
{
    public interface IUserRepository
    {
        User getById(Guid userId);
        User GetUserByEmail(string email, string password);
        Task<User> registerAsync(User user);
		User getuser();
		Task<User> Update(User user);
        User memberRegister(User user);
        List<User> memberListing(Guid companyId);

        List<User> getAllUsers();

        void memberDeleteById(Guid id);
		bool IsUserExist(string email);
	}
}
