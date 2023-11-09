using System.Security.Principal;

namespace Ofgem.GBI.InternalPortal.Service.Configuration
{
    public class UserContext : IUserContext
    {
        public string? HashedAccountId { get; set; }
        public IPrincipal? User { get; set; }
    }
}
