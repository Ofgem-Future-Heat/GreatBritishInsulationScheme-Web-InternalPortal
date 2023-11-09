namespace Ofgem.GBI.InternalPortal.Service.Configuration
{
    public class FooterConfiguration : IFooterConfiguration
    {
        public string? ApplicationBaseUrl { get; set; }
        public string? AuthenticationAuthorityUrl { get; set; }
    }
}
