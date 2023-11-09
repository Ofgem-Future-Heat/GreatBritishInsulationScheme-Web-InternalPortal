
namespace Ofgem.GBI.InternalPortal.Service.Configuration
{
    public interface IFooterConfiguration
    {
        string? ApplicationBaseUrl { get; set; }
        string? AuthenticationAuthorityUrl { get; set; }
    }
}
