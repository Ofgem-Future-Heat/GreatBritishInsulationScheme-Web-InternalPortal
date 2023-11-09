using Ofgem.Web.GBI.InternalPortal.UI.Services;

namespace Ofgem.GBI.InternalPortal.Service.Models.Links
{
    public class ExternalUsers : Link
    {
        private readonly bool _isSelected;

        public ExternalUsers(string href, string @class = "", bool isSelected = false) : base(href, @class: @class)
        {
            _isSelected = isSelected;
        }

        public override string Render()
        {
            return LinkService.CreateMainMenuListItemLink(_isSelected, Href, "External users");
        }
    }
}
