using Ofgem.GBI.InternalPortal.Service.Configuration;
using Ofgem.GBI.InternalPortal.Service.Services;

namespace Ofgem.GBI.InternalPortal.Service.Models
{
    public interface IHeaderViewModel : ILinkCollection, ILinkHelper
    {
        bool MenuIsHidden { get; }
        string SelectedMenu { get;}
        IUserContext UserContext { get; }
        void HideMenu();
        void SelectMenu(string menu);
        bool UseLegacyStyles { get; }
        string PhaseBannerTag { get; }
    }
}
