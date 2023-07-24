using Api.Test.Fixtures;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Controllers
{
	public class InterviewControllerTest
	{
		protected readonly HttpClient _httpClient;

		public InterviewControllerTest()
		{
			ApiWebApplicationFactory _factory = new ApiWebApplicationFactory();
			_httpClient = _factory.CreateClient();
		}
		[Fact]
		public async Task POST_interview_Shedule_Success()
		{
			//Arrange  
			InterviewDto interviewdto = new InterviewDto(Guid.NewGuid(),"TCS", "Senior dotnet developer", "10/12/2023", "TCR","10.00");
			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(interviewdto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PostAsync("interview/interviewShedule", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);


		}
		[Fact]
		public async Task POST_interview_Shedule_without_CompanyName_BadRequest()
		{
			InterviewDto interviewdto = new InterviewDto(Guid.NewGuid(),null, "Senior dotnet developer", "10/12/2023", "TCR", "10.00");
			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(interviewdto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PostAsync("interview/interviewShedule", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}






	} 
}
