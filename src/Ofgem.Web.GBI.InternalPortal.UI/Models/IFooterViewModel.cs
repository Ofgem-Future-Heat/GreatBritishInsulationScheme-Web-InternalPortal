using Ofgem.GBI.InternalPortal.Service.Services;

namespace Ofgem.GBI.InternalPortal.Service.Models
{
    public interface IFooterViewModel : ILinkCollection, ILinkHelper
    {
        bool UseLegacyStyles { get; }
    }
}
