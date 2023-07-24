using AutoMapper;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Exceptions;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireMeNowWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiVersion("1.0")]
	[ApiController]
	public class PublicController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public PublicController(IUserService userService, IMapper mapper, IUserRepository userRepository)
		{
			_userService=userService;
			_mapper=mapper;
			_userRepository=userRepository;
		}

		[HttpPost]
		[Route("register")]
		public IActionResult Register(UserDto userDto)
		{
			if (_userRepository.IsUserExist(userDto.Email))
			{
				return BadRequest("User Already Exist");
			}
			
				return Ok(_userService.register(userDto));
			
		}

		[HttpPost]
		[Route("login")]
		public IActionResult Login(LoginDto loginDto)
		{
			//var user = _mapper.Map<User>(userDto);
			var user = _userService.login(loginDto.Email, loginDto.Password);
			if (user == null)
			{
				return BadRequest("Login Failed");
			}
			return Ok(_mapper.Map<UserDto>(user));
		}
	}
}
