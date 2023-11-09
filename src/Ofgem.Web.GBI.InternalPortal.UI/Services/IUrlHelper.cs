using Ofgem.GBI.InternalPortal.Service.Configuration;

namespace Ofgem.GBI.InternalPortal.Service.Services
{
    public interface IUrlHelper
    {
        string GetPath(string baseUrl, string path = "");
        string GetPath(IUserContext userContext, string baseUrl, string path = "", string prefix = "");
    }
}
