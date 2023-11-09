using Moq;
using Moq.Protected;

namespace Ofgem.Web.GBI.InternalPortal.UI.UnitTests.Mocks
{
    public class MockHttpMessageHandler : Mock<HttpMessageHandler>
    {
        public MockHttpMessageHandler MockSendAsync(HttpResponseMessage response)
        {
            this.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(response));

            return this;
        }
    }
}
