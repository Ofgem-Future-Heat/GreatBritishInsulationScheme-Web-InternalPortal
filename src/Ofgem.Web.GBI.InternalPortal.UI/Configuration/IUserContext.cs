
using System.Security.Principal;

namespace Ofgem.GBI.InternalPortal.Service.Configuration
{
    public interface IUserContext
    {
        string? HashedAccountId { get; set; }
        IPrincipal? User { get; set; }
    }
}
