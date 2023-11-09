using Microsoft.Extensions.Logging;
using Moq;
using Ofgem.Web.GBI.InternalPortal.Infrastructure.Services;
using Ofgem.Web.GBI.InternalPortal.UI.UnitTests.Mocks;
using System.Net;

namespace Ofgem.Web.GBI.InternalPortal.UI.UnitTests
{
    public class SupplierAccountServiceTests
    {
        private readonly Uri _baseAddress = new("https://localhost");

        [Fact]
        public async void GetUsers_WhenUsersExists_ReturnsUsers()
        {
            // Arrange
            string responseData = File.ReadAllText("Data/get-suppliers.json");
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseData)
            };

            var httpMessageHandler = new MockHttpMessageHandler().MockSendAsync(responseMessage);

            var httpClient = new HttpClient(httpMessageHandler.Object)
            {
                BaseAddress = _baseAddress
            };

            var supplierAccountService = new SupplierAccountsService(httpClient, new Mock<ILogger<SupplierAccountsService>>().Object);

            // Act
            IDictionary<int, string>? suppliers = await supplierAccountService.GetSupplierAccountsAsync();

            // Assert
            Assert.NotNull(suppliers);
            Assert.Equal(35, suppliers.Count);
        }
    }
}
