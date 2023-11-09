using Ofgem.GBI.InternalPortal.Service.Configuration;
using Ofgem.GBI.InternalPortal.Service.Models.Links;
using Ofgem.GBI.InternalPortal.Service.Services;

namespace Ofgem.GBI.InternalPortal.Service.Models
{
    public class HeaderViewModel : IHeaderViewModel
    {
        public IUserContext UserContext { get; private set; }
        public bool MenuIsHidden { get; private set; }
        public string SelectedMenu { get; private set; }

        public IReadOnlyList<Link> Links => _linkCollection.Links;

        public bool UseLegacyStyles { get; private set; }

        private readonly ILinkCollection _linkCollection;
        private readonly ILinkHelper _linkHelper;
        private readonly string? phaseBannerTag;

        public HeaderViewModel(
            IHeaderConfiguration configuration,
            IUserContext userContext,
            ILinkCollection? linkCollection = null,
            ILinkHelper? linkHelper = null,
            IUrlHelper? urlHelper = null,
            bool useLegacyStyles = false,
            string? currentUrl = null)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            UserContext = userContext ?? throw new ArgumentNullException(nameof(userContext));

            _linkCollection = linkCollection ?? new LinkCollection();
            _linkHelper = linkHelper ?? new LinkHelper(_linkCollection);
            IUrlHelper _urlHelper = urlHelper ?? new UrlHelper();
            UseLegacyStyles = useLegacyStyles;

            MenuIsHidden = false;
            SelectedMenu = "home";

            // Header links
            AddOrUpdateLink(new Links.GovUk("/", "govuk-header__service-name"));
            AddOrUpdateLink(new GbisHome("/", "govuk-header__service-name"));

            // Main navigation links
            AddOrUpdateLink(new Home(_urlHelper.GetPath(userContext, configuration.HomeUrl!, "Index"), isSelected: new string[] { @"/Index", @"/" }.Contains(currentUrl)));
            AddOrUpdateLink(new ExternalUsers(_urlHelper.GetPath(userContext, configuration.ExternalUsersUrl!, "ExternalUsers"), isSelected: new string[] { @"/ExternalUsers", @"/ExternalUserEdit", @"/ExternalUserAdd", @"/ExternalUserView", @"/ExternalUserConfirm", "/ExternalUserSaved", "/ExternalUserDeleted" }.Contains(currentUrl)));

            AddOrUpdateLink(new Feedback("#", "govuk-link"));
            phaseBannerTag = configuration.PhaseBannerTag;
        }


        public void HideMenu()
        {
            MenuIsHidden = false;
        }

        public void SelectMenu(string menu)
        {
            SelectedMenu = menu;
        }

        public void AddOrUpdateLink<T>(T link) where T : Link
        {
            _linkCollection.AddOrUpdateLink(link);
        }

        public void RemoveLink<T>() where T : Link
        {
            _linkCollection.RemoveLink<T>();
        }

        public string RenderListItemLink<T>(bool isSelected = false, string @class = "") where T : Link
        {
            return _linkHelper.RenderListItemLink<T>(isSelected, @class);
        }

        public string RenderLink<T>(Func<string>? before = null, Func<string>? after = null, bool isSelected = false) where T : Link
        {
            return _linkHelper.RenderLink<T>(before, after, isSelected);
        }

        public string PhaseBannerTag { get { return phaseBannerTag!; } }
    }
}
