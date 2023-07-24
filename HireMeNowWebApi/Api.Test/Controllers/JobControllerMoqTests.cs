using AutoMapper;
using HireMeNowWebApi.Controllers;
using HireMeNowWebApi.Helpers;
using HireMeNowWebApi.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HireMeNowWebApi.Models;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Data.UnitOfWorks;

namespace Api.Test.Controllers
{
	public class JobControllerMoqTests
	{
		//private readonly Mock<IJobRepository> _mockRepo;
		private readonly Mock<IUnitOfWork> _mockUnitOfWorkRepo;
		private readonly JobController _controller;
		private readonly IMapper _mapper;
		private readonly JobDto jobdto= new JobDto {Id=new Guid("ed3d8914-a950-4f51-0275-08db82d180bf"), Title="Phython Developer", Description= "Senior dotnet developer .", Location= "kochi", TypeOfWork= "Fulltime", Salary= "100000-300000", CompanyId= new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89") };
		
		private readonly List<Job> jobs = new List<Job> {
				new Job{Id=new Guid("7163744e-eb8d-45a4-82a8-2c7816f4526d"),Title="Dotnet Developer",Description= "Senior dotnet developer .",Location= "kochi",JobType= "Fulltime",Salary= "100000-300000",CompanyId= new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89") },
				new Job{Id=new Guid("8e0095ce-f90c-4f03-003f-08db844f473f"),Title="Java Developer",Description= "Senior dotnet developer .",Location= "kochi",JobType= "Fulltime",Salary= "100000-300000",CompanyId= new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89") },
				new Job{Id=new Guid("e86a5bb8-3c03-4591-b214-8087dd605da5"),Title="Angular Developer",Description= "Senior dotnet developer .",Location= "kochi",JobType= "Fulltime",Salary= "100000-300000",CompanyId= new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89") } };

		public JobControllerMoqTests()
		{
			//_mockRepo = new Mock<IJobRepository>();
			_mockUnitOfWorkRepo=new Mock<IUnitOfWork>();
			var configurationProvider = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<AutoMapperProfiles>();
			});
			_mapper = configurationProvider.CreateMapper();
			_controller = new JobController(_mapper, _mockUnitOfWorkRepo.Object);

		}


		[Fact]
		public async Task GET_Jobs_Results_Success_Count_3()
		{
			//Arrange  
			JobListParams param =new JobListParams();
			var listdata =  new PagedList<Job>(jobs, jobs.Count, param.PageNumber, param.PageSize);
			_mockUnitOfWorkRepo.Setup(repo => repo.JobRepository.GetAllByFilter(param)).ReturnsAsync(listdata);

			//Act
			var result = await _controller.GetJobAsync(param);
			Assert.NotNull(result);
			OkObjectResult response = (OkObjectResult)result;

			//Assert
			var res =(List<JobDto>) response.Value;
			Assert.Equal(200, response.StatusCode);
			Assert.Equal(res.Count, listdata.Count);

		}

		[Fact]
		public async Task GET_Job_By_Id_Results_Success()
		{
			//Arrange  
			Guid jobId = new Guid("e86a5bb8-3c03-4591-b214-8087dd605da5");
			Job jobToReturn = new Job { Id=new Guid("7163744e-eb8d-45a4-82a8-2c7816f4526d"), Title="Dotnet Developer", Description= "Senior dotnet developer .",
							  Location= "kochi", JobType= "Fulltime", Salary= "100000-300000", CompanyId= new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89") };

			_mockUnitOfWorkRepo.Setup(repo => repo.JobRepository.GetJobById(jobId)).Returns(jobToReturn);
			
		//Act
			var result =  _controller.GetJob(jobId);
			Assert.NotNull(result);
			OkObjectResult response = (OkObjectResult)result;

			//Assert
			Assert.Equal(200, response.StatusCode);
			var res = (JobDto)response.Value;
			
			Assert.Equal(res.Id, jobToReturn.Id);
			Assert.Equal(res.Title, jobToReturn.Title);
			Assert.Equal(res.Description, jobToReturn.Description);
			Assert.Equal(res.Location, jobToReturn.Location);
			Assert.Equal(res.Salary, jobToReturn.Salary);

		}

		[Fact]
		public async Task POST_Job_Results_Success()
		{
			//Arrange  
			Job jobToCreate = new Job
			{
				Id=new Guid("7163744e-eb8d-45a4-82a8-2c7816f4526d"),
				Title="Dotnet Developer",
				Description= "Senior dotnet developer .",
				Location= "kochi",
				JobType= "Fulltime",
				Salary= "100000-300000",
				CompanyId= new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89")
			};


			_mockUnitOfWorkRepo.Setup(repo => repo.JobRepository.Create(jobToCreate));

			//Act
			var result = await _controller.PostJobAsync(jobdto);

			//Assert
			Assert.NotNull(result);
			CreatedResult response = (CreatedResult)result;
			Assert.Equal(201, response.StatusCode);
			
		}

		[Fact]
		public async Task PUT_Job_Results_Success()
		{
			//Arrange  
			Job jobToCreate = new Job
			{
				Id=new Guid("7163744e-eb8d-45a4-82a8-2c7816f4526d"),
				Title="Dotnet Developer",
				Description= "Senior dotnet developer .",
				Location= "kochi",
				JobType= "Fulltime",
				Salary= "100000-300000",
				CompanyId= new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89")
			};


			_mockUnitOfWorkRepo.Setup(repo => repo.JobRepository.UpdateAsync(jobToCreate));

			//Act
			var result = await _controller.Update(jobdto, new Guid("7163744e-eb8d-45a4-82a8-2c7816f4526d"));

			//Assert
			Assert.NotNull(result);
			OkObjectResult response = (OkObjectResult)result;
			Assert.Equal(200, response.StatusCode);

		}
	}
}
