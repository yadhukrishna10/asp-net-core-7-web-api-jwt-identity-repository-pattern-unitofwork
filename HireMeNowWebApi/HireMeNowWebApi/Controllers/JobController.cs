using AutoMapper;
using HireMeNowWebApi.Data.UnitOfWorks;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Enums;
using HireMeNowWebApi.Extensions;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HireMeNowWebApi.Controllers
{
	
	[ApiController]
	[ApiVersion("1.0")]
	//[Route("api/v{version:apiVersion}/[controller]")]
	[Route("api/[controller]")]
	//[Authorize(Roles = "ADMIN")]
	public class JobController : ControllerBase
	{

		private readonly IJobService _jobService;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		IJobRepository _jobRepository;
		// GET: api/<JobController>
		//public JobController(IJobService jobService, IMapper mapper , IUnitOfWork unitOfWork)
		//      {
		//	 _jobService= jobService;
		//	_mapper= mapper;
		//	_unitOfWork= unitOfWork;

		//}
		public JobController(IMapper mapper, IUnitOfWork unitOfWork)
        {
			
			_mapper= mapper;
			_unitOfWork= unitOfWork;
		}

		[HttpGet("/jobs")]
		public async Task<IActionResult> GetJobAsync([FromQuery] JobListParams param)
		{
			//if (param.JobType < 0 || param.JobType  > 4) return BadRequest("Invalid Message Type");

			var jobslist = await _unitOfWork.JobRepository.GetAllByFilter(param);
		    // Response.AddPaginationHeader(jobslist.CurrentPage, jobslist.PageSize, jobslist.TotalCount, jobslist.TotalPages);
			List<JobDto> job = _mapper.Map<List<JobDto>>(jobslist);
			return Ok(job);
		}
		//[HttpGet("/job/GetJobListByid")]
		[HttpGet]
		[Route("jobs/{jobid}")]
		public IActionResult GetJob(Guid jobid)
		{
			Job job = _unitOfWork.JobRepository.GetJobById(jobid);
			return Ok(_mapper.Map<JobDto>(job));
		}
		//[HttpGet]
		//[ApiVersion("2.0")]
		//[Route("get-joblist-by-id")]
		//public IActionResult GetJob2(Guid selectedJobId)
		//{
		//	Job job = _jobService.getJobById(selectedJobId);
		//	return Ok(job);
		//}
		// GET api/<JobController>/5
		//[HttpGet("/job/GetByTitle")]
		//public IActionResult Get(string title)
		//{
		//	List<Job> job = _jobRepository.getByTitle(title);
		//	if (job == null)
		//	{
		//		return BadRequest("User with id : " + title + " Not Found.");
		//	}
		//	return Ok(job);
		//}

		//// POST api/<JobController>
		[HttpPost]
		public async Task<IActionResult> PostJobAsync(JobDto jobDto)
		{
			var job = _mapper.Map<Job>(jobDto);
			_unitOfWork.JobRepository.Create(job);
			await _unitOfWork.Complete();
			return Created("", jobDto);

		}

		//// PUT api/<JobController>/5
		[HttpPut]
		[Route("job/{id}")]
		public async Task<IActionResult> Update(JobDto jobdto,Guid id)
		{
			jobdto.Id=id;
			var jobToUpdate = _mapper.Map<Job>(jobdto);
			Job job =await _unitOfWork.JobRepository.UpdateAsync(jobToUpdate);
			
			return Ok(_mapper.Map<JobDto>(job));
		}

		//// DELETE api/<JobController>/5
		//[HttpDelete("{id}")]
		//public IActionResult Remove(Guid id)
		//{
		//	_jobService.DeleteItemById(id);

		//	return NoContent();
		//}
	}
}
