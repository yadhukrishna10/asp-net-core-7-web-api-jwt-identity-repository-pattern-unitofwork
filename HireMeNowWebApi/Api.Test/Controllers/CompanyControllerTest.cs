using Api.Test.Fixtures;
using HireMeNowWebApi.Dtos;
using HireMeNowWebApi.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Api.Test.Controllers
{
    public class CompanyControllerTest
    {
        protected readonly HttpClient _httpClient;

        public CompanyControllerTest()
        {
            ApiWebApplicationFactory _factory = new ApiWebApplicationFactory();
            _httpClient = _factory.CreateClient();

        }

        [Fact]
        public async Task POST_Register_member_without_email_Results_BadRequest()
        {
            //Arrange  
            UserDto userDto = new UserDto("anshid", "ansar", "", "male", "thrissur", 9633508643, "123", Roles.COMPANY_MEMBER);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            //Act
            var response = await _httpClient.PostAsync("company/memberRegister", httpContent);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
        [Fact]
        public async Task POST_Register_member_Without_Password_Results_BadRequest()
        {
            //Arrange  
            UserDto userDto = new UserDto("yadhu", "krishna", "", "male", "thrissur", 9633508643, null, Roles.JOB_PROVIDER);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            //Act
            var response = await _httpClient.PostAsync("company/memberRegister", httpContent);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task POST_Register_company_member_Results_Success()
        {
            //Arrange  
            UserDto userDto = new UserDto("yadhu", "krishna", "yadhu@gmail.com", "male", "thrissur", 9633508643, "123", Roles.JOB_PROVIDER);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            //Act
            var response = await _httpClient.PostAsync("company/memberRegister", httpContent);
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
        [Fact]
        public async Task Get_Listing_Company_Members_Without_CompanyId_Results_BadRequest()
        {
            //Arrange  
            UserDto userDto = new UserDto("yadhu", "krishna", "yadhu@gmail.com", "male", "thrissur", 9633508643, "123", Roles.JOB_PROVIDER);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //Act
            var response = await _httpClient.GetAsync("company/memberListing?companyId=");
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task Get_Listing_Company_Members_With_CompanyId_Results_Success()
        {
            //Arrange  
            //UserDto userDto = new UserDto("yadhu", "krishna", "yadhu@gmail.com", "male", "thrissur", 9633508643, "123", Roles.JobProvider);
            //HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            //Act
            var response = await _httpClient.GetAsync("company/memberListing?companyId=1d8303fb-c1e1-4fa6-a2e1-272472b2beb4");
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Remove_ValidId_ReturnsNoContent()
        {
            // Arrange
            Guid userId = new Guid("1d8303fb-c1e1-4fa6-a2e1-272472b2beb4");

            // Act
            var response = await _httpClient.DeleteAsync($"/company/RemoveMember?id={userId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        [Fact]
        public async Task Remove_EmptyId_ReturnBadRequest()
        {
            // Arrange
            Guid? userId =null;

            // Act
            var response = await _httpClient.DeleteAsync($"/company/RemoveMember?id={userId}");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

		[Fact]
		public async Task POST_Register_Company_Results_Success()
		{
			//Arrange  
			CompanyDto companyDto = new CompanyDto();
			companyDto.Name="Aitrich Technologies";
			companyDto.Email="aitrich.info@aitrich.com";
			companyDto.Website="www.aitrich.com";
			companyDto.Phone="9877890987";


			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(companyDto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PostAsync("company/register", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		}
		[Fact]
		public async Task POST_Register_Company_Without_Name_Results_BadRequest()
		{
			//Arrange  
			CompanyDto companyDto = new CompanyDto();
			//companyDto.Name="Aitrich Technologies";
			companyDto.Email="aitrich.info@aitrich.com";
			companyDto.Website="www.aitrich.com";
			companyDto.Phone="9877890987";


			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(companyDto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PostAsync("company/register", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

		}

		[Fact]
		public async Task GET_Company_List_Results_Success()
		{
			//Arrange  


			//Act
			var response = await _httpClient.GetAsync("company/list");

			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		}
		[Fact]
		public async Task GET_Company_List_With_Name_Filter_Results_Success()
		{
			//Arrange  

			//Act
			var response = await _httpClient.GetAsync("company/list?name=Aitrich");

			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		}
		[Fact]
		public async Task GET_Company_List_With_Id_Filter_Results_Success()
		{
			//Arrange  

			//Act
			var response = await _httpClient.GetAsync("company/list?id=2c8303fb-c1e1-4fa6-a2e1-272472b4beb5");

			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);

		}

		[Fact]
		public async Task PUT_Company_Profile_Results_Success()
		{
			//Arrange
			CompanyDto companyDto = new CompanyDto("Aitrich", "aitrich@gmail.com", "www.aitrich.com", "7654643632", null, "IT Training and consultancy service .", "To Guid Students to Future of web development", "Our Mission", "Thrissur", "2nd Floor , Parayil Builders,Mg Road , Thrissur ", new Guid("2c8303fb-c1e1-4fa6-a2e1-272472b4beb5"));

			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(companyDto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PutAsync("company/profile", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
			var res = response.Content.ReadAsStringAsync().Result;
			CompanyDto updatedCompany = JsonConvert.DeserializeObject<CompanyDto>(res);

			Assert.Equal(companyDto.Name, updatedCompany.Name);
			Assert.Equal(companyDto.Phone, updatedCompany.Phone);
			Assert.Equal(companyDto.Email, updatedCompany.Email);
			Assert.Equal(companyDto.About, updatedCompany.About);
			Assert.Equal(companyDto.Location, updatedCompany.Location);
			Assert.Equal(companyDto.Mission, updatedCompany.Mission);

		}

		[Fact]
		public async Task PUT_Company_Profile_Without_Id_Results_BadRequest()
		{
			//Arrange
			CompanyDto companyDto = new CompanyDto("Aitrich", "aitrich@gmail.com", "www.aitrich.com", "7654643632", null, "IT Training and consultancy service .", "To Guid Students to Future of web development", "Our Mission", "Thrissur", "2nd Floor , Parayil Builders,Mg Road , Thrissur ");
			companyDto.Id=null;
			HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(companyDto), Encoding.UTF8);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


			//Act
			var response = await _httpClient.PutAsync("company/profile", httpContent);
			//Assert
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);


		}

	}
}
