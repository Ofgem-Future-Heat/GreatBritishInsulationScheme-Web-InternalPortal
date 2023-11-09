using Ofgem.GBI.InternalPortal.Service.Configuration;

namespace Ofgem.GBI.InternalPortal.Service.Services
{
    public class UrlHelper : IUrlHelper
    {
        public string GetPath(string baseUrl, string path = "")
        {
            var trimmedBaseUrl = baseUrl?.TrimEnd('/') ?? string.Empty;

            return $"{trimmedBaseUrl}/{path}".TrimEnd('/');
        }

        public string GetPath(IUserContext userContext, string baseUrl, string path = "", string prefix="")
        {
            prefix = string.IsNullOrEmpty(prefix) ? string.Empty : prefix + '/';
        
            var trimmedBaseUrl = baseUrl?.TrimEnd('/') ?? string.Empty;

            return $"{trimmedBaseUrl}".TrimEnd('/');
        }
    }
}
