using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Api.Test.Fixtures;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace Api.Test.Controllers
{
	public class JobControllerTest
	{
		protected readonly HttpClient _httpClient;
		public JobControllerTest()
		{
			ApiWebApplicationFactory _factory = new ApiWebApplicationFactory();
			_httpClient = _factory.CreateClient();
			//string accessToken = getAccessToken();
			//_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

		}
		[Fact]
		public async Task POST_Job_Results_Success()
		{
			//Arrange  
			JobDto jobDto = new JobDto("Java Developer", "Senior dotnet developer", "kochi","100000-300000", Guid.NewGuid(), "Aitrich");
			jobDto.Experience = "2-3 years";
			jobDto.TypeOfWork = "Online";
			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(jobDto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PostAsync("job/PostJob", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		}
		[Fact]
		public async Task Get_PostJob_without_Title_BadRequest()

		{
			//Arrange  
			JobDto jobDto = new JobDto(null, "Senior dotnet developer", "kochi", "100000-300000", Guid.NewGuid(), "Aitrich");
			jobDto.Experience = "2-3 years";
			jobDto.TypeOfWork = "Online";
			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(jobDto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PostAsync("job/PostJob", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

		}


		[Fact]
		public async Task GET_Job_Results_Success()
		{
			//Arrange  
			var jobId = "62ec44fb-9f30-4f45-8e3d-f3751998af89";


			//Act
			var response = await _httpClient.GetAsync("/job/GetJobListByid?selectedJobId="+jobId);
			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		}
		[Fact]
		public async Task Remove_ValidId_ReturnsNoContent()
		{
			// Arrange
			var jobId = new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89");

			// Act
			var response = await _httpClient.DeleteAsync($"/api/Job/{jobId}");

			// Assert
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}
		[Fact]
		public async Task Remove_EmptyId_ReturnBadRequest()
		{
			// Arrange
			var jobId = Guid.Empty;

			// Act
			var response = await _httpClient.DeleteAsync($"/api/Job/{jobId}");

			// Assert
			Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
		}
		[Fact]
		public async Task PUT_Job_Results_Success()
		{
			//Arrange
			JobDto jobDto = new JobDto("Java Developer", "Senior dotnet developer", "kochi", "100000-300000", Guid.NewGuid(), "Aitrich");
			jobDto.Experience = "2-3 years";
			jobDto.TypeOfWork = "Online";
			
			jobDto.Id = new Guid("62ec44fb-9f30-4f45-8e3d-f3751998af89");
			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(jobDto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PutAsync("/job/UpdateJob", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var res = response.Content.ReadAsStringAsync().Result;
			JobDto updatedjob = JsonConvert.DeserializeObject<JobDto>(res);

			Assert.Equal(jobDto.Title, updatedjob.Title);
			Assert.Equal(jobDto.Description, updatedjob.Description);
			Assert.Equal(jobDto.Location, updatedjob.Location);
			Assert.Equal(jobDto.Salary, updatedjob.Salary);
			Assert.Equal(jobDto.Experience, updatedjob.Experience);
			Assert.Equal(jobDto.TypeOfWork, updatedjob.TypeOfWork); 
			Assert.Equal(jobDto.CompanyName, updatedjob.CompanyName);
		}

	}
}
