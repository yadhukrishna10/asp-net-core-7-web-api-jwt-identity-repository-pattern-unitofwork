using AutoMapper;
using Azure.Core;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using System.Security.Claims;

namespace HireMeNowWebApi.Services
{
    public class UserService : IUserService
    {
        public IUserRepository userRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private AuthHelper _authHelper;
        private IMapper _mapper;
        public UserService(IUserRepository _userRepository,AuthHelper authHelper, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            userRepository = _userRepository;
			_authHelper= authHelper;
            _mapper=mapper;
            _httpContextAccessor= httpContextAccessor;
		}
        public UserProfileDto GetCurrentUser()
        {
            string userid = GetUserId()??throw new UnauthorizedAccessException();
   //         string email = GetCurrentUserEmail();
   //         string role = GetCurrentUserRole();
			//string name=GetCurrentUsername();
			User user= userRepository.getById(new Guid(userid));
			return _mapper.Map<UserProfileDto>(user);

		}

		public string GetCurrentUsername()
		{
			var result = string.Empty;
			if (_httpContextAccessor.HttpContext != null)
			{
				result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
			}
			return result;
		}

		public string GetCurrentUserRole()
		{
			var result = string.Empty;
			if (_httpContextAccessor.HttpContext != null)
			{
				result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
			}
			return result;
		}

		public string GetCurrentUserEmail()
		{
			var result = string.Empty;
			if (_httpContextAccessor.HttpContext != null)
			{
				result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
			}
			return result;
		}

		public string GetUserId()
		{
			var result = string.Empty;
			if (_httpContextAccessor.HttpContext != null)
			{
				result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
			}
			return result;
		}

		public User getById(Guid userId)
        {
            return userRepository.getById(userId);
        }
		public List<User> getAllUsers()
		{
			return userRepository.getAllUsers();
		}

		public UserDto login(string email, string password)
        {
            var user = userRepository.GetUserByEmail(email, password);
           if( _authHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            { var userReturn= _mapper.Map<UserDto>(user);
                userReturn.Token=_authHelper.CreateToken(user);
				return userReturn;
            }
            return null;

		}

        public async Task<User> register(UserDto userdto)
        {
			_authHelper.CreatePasswordHash(userdto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            User user = _mapper.Map<User>(userdto);
            user.PasswordHash=passwordHash;
            user.PasswordSalt=passwordSalt;
			user.Role=Enums.Roles.JOBSEEKER;
			return await userRepository.registerAsync(user);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var updateduser= await userRepository.Update(user);
            return updateduser;
        }

	
	}
}
