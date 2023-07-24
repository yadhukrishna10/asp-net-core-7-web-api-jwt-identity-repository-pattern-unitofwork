using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Api.Test.Fixtures;
using Newtonsoft.Json;
using HireMeNowWebApi.Dtos;
using System.Net.Http.Headers;
using HireMeNowWebApi.Enums;

namespace Api.Test.Controllers
{
    public class UserControllerTest
    {
        protected readonly HttpClient _httpClient;
        public UserControllerTest()
        {
            ApiWebApplicationFactory _factory = new ApiWebApplicationFactory();
            _httpClient = _factory.CreateClient();
            //string accessToken = getAccessToken();
            //_httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

        }

        [Fact]
        public async Task POST_Register_user_without_email_Results_BadRequest()
        {
            //Arrange  
            UserDto userDto = new UserDto("yadhu", "krishna", "", "male", "thrissur", 9633508643, "123", Roles.JOB_PROVIDER);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            //Act
            var response = await _httpClient.PostAsync("account/register", httpContent);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
        [Fact]
        public async Task POST_Register_User_Without_Password_Results_BadRequest()
        {
            //Arrange  
            UserDto userDto = new UserDto("yadhu", "krishna", "", "male", "thrissur", 9633508643, null, Roles.JOB_PROVIDER);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            //Act
            var response = await _httpClient.PostAsync("account/register", httpContent);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task POST_Register_user_Results_Success()
        {
            //Arrange  
            UserDto userDto = new UserDto("yadhu","krishna","yadhu@gmail.com","male","thrissur",9633508643,"123",Roles.JOB_PROVIDER) ;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

           
            //Act
            var response = await _httpClient.PostAsync("account/register", httpContent);
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
        [Fact]
        public async Task GET_User_Profile_Results_Success()
        {
            //Arrange  
            var userId = "3fa85f64-5717-4562-b3fc-2c963f66afa6";


            //Act
            var response = await _httpClient.GetAsync("/account/profile?userId="+userId);
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
        [Fact]
        public async Task GET_User_Profile_Wrong_UserId_Results_BadRequest()
        {
            //Arrange  
            var userId = "3fa85f64-5717-4562-b3fc-2c963f89afa6";


            //Act
            var response = await _httpClient.GetAsync("/account/profile?userId="+userId);
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task PUT_User_Profile_Results_Success()
        {
            //Arrange
            UserDto userDto = new UserDto("yadhu", "krishna", "yadhu@gmail.com", "male", "thrissur", 9633508643, "123", Roles.JOB_PROVIDER);
            userDto.Id=new Guid("1d8303fb-c1e1-4fa6-a2e1-272472b2beb4");
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            //Act
            var response = await _httpClient.PutAsync("account/profile", httpContent);
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var res = response.Content.ReadAsStringAsync().Result;
            UserDto updatedUser = JsonConvert.DeserializeObject<UserDto>(res);

            Assert.Equal(userDto.FirstName, updatedUser.FirstName);
            Assert.Equal(userDto.LastName, updatedUser.LastName);
            //Assert.Equal(userDto.Email, updatedUser.Email);
            Assert.Equal(userDto.Gender, updatedUser.Gender);
            Assert.Equal(userDto.Location, updatedUser.Location);
            Assert.Equal(userDto.Phone, updatedUser.Phone);

        }

        [Fact]
        public async Task PUT_User_Profile_With_Wrong_UserID_Results_BadRequest()
        {
            //Arrange
            UserDto userDto = new UserDto("yadhu", "krishna", "yadhu@gmail.com", "male", "thrissur", 9633508643, "123", Roles.JOB_PROVIDER);
            userDto.Id=new Guid("1d8303fb-c1e1-4fa6-a2e1-272472b2beb3");
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            //Act
            var response = await _httpClient.PutAsync("account/profile", httpContent);
            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}
