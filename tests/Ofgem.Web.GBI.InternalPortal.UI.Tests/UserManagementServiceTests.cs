using Microsoft.Extensions.Logging;
using Moq;
using Ofgem.Web.GBI.InternalPortal.Domain.Models;
using Ofgem.Web.GBI.InternalPortal.Infrastructure.Services;
using Ofgem.Web.GBI.InternalPortal.UI.UnitTests.Mocks;
using System.Net;

namespace Ofgem.Web.GBI.InternalPortal.UI.UnitTests
{
    public class UserManagementServiceTests
    {
        private readonly Uri _baseAddress = new("https://localhost");

        [Fact]
        public async void GetUsers_WhenUsersExists_ReturnsUsers()
        {
            // Arrange
            string responseData = File.ReadAllText("Data/get-external-users.json");
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseData)
            };

            var userManagementService = new UserManagementService(
                CreateHttpClient(responseMessage), new Mock<ILogger<UserManagementService>>().Object);

            // Act
            IEnumerable<UserDetailModel>? users = await userManagementService.GetUsersAsync();

            // Assert
            Assert.NotNull(users);
            Assert.Equal(2, users!.Count());
        }

        [Fact]
        public async void GetUser_WhenUserExists_ReturnsUser()
        {
            // Arrange
            string responseData = File.ReadAllText("Data/get-external-user.json");
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseData)
            };

            var userManagementService = new UserManagementService(
                CreateHttpClient(responseMessage), new Mock<ILogger<UserManagementService>>().Object);

            // Act
            UserDetailModel? user = await userManagementService.GetUserAsync(Guid.NewGuid());

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public async void SaveUser_WithValidRequest_ReturnsUserId()
        {
            // Arrange
            string responseData = File.ReadAllText("Data/save-external-user.json");
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseData)
            };

            var userManagementService = new UserManagementService(
                CreateHttpClient(responseMessage), new Mock<ILogger<UserManagementService>>().Object);

            // Act
            Guid userId = await userManagementService.SaveUserAsync(new SaveExternalUserRequest());

            // Assert
            Assert.False(userId == Guid.Empty);
        }

        private HttpClient CreateHttpClient(HttpResponseMessage httpResponseMessage)
        {
            var httpMessageHandler = new MockHttpMessageHandler().MockSendAsync(httpResponseMessage);

            return new HttpClient(httpMessageHandler.Object)
            {
                BaseAddress = _baseAddress
            };
        }
    }
}
