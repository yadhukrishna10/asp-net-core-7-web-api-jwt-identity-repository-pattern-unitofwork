using AutoMapper;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Repositories;
using HireMeNowWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireMeNowWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "JOBSEEKER")]
	public class JobSeekerController : ControllerBase
	{

		IJobService _jobService;
		IUserService _userService;
		IUserRepository _userRepository;
		IApplicationService _applicationService;
		public JobSeekerController(IJobService jobService, IUserService userService, IUserRepository userRepository, IApplicationService applicationService)
		{
			_jobService = jobService;
			_userService = userService;
			_userRepository = userRepository;
			_applicationService = applicationService;
		}
		[HttpPost]
		[Route("ApplyJob")]
		public IActionResult ApplyJob(Guid jobId)
		{
			if (jobId != null)
			{

				//bool res=_userService.ApplyJob(new Guid(jobId),new Guid(uid));
				var UserId = _userService.GetUserId();
				if(UserId == null)
				{
					return Unauthorized();
				}
				_applicationService.AddApplication(jobId, new Guid(UserId));


			}
			return NoContent();
		}

		//[HttpGet("/AllJobs")]
		//public async Task<IActionResult> AllJobs()
		//{
		//	var result = _userRepository.getuser();
		//	//HttpContext.Session.SetString("UserId", result.Id.ToString());
		//	List<Job> jobs =await _jobService.GetJobs();

		
		//	return Ok(jobs);
		//}

	}

}
