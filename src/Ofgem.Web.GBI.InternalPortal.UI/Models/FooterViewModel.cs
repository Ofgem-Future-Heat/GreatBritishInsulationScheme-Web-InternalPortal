using Ofgem.GBI.InternalPortal.Service.Models.Links;
using Ofgem.GBI.InternalPortal.Service.Services;

namespace Ofgem.GBI.InternalPortal.Service.Models
{
    public class FooterViewModel : IFooterViewModel
    {
        const string BuiltByHRef = "https://www.ofgem.gov.uk/";
        const string OpenGovernmentLicenseHRef = "https://www.nationalarchives.gov.uk/doc/open-government-licence/version/3/";
        const string CrownCopyrightHRef = "https://www.nationalarchives.gov.uk/information-management/re-using-public-sector-information/uk-government-licensing-framework/crown-copyright/";

        public IReadOnlyList<Link> Links => _linkCollection.Links;

        public bool UseLegacyStyles { get; private set; }

        private readonly ILinkCollection _linkCollection;
        private readonly ILinkHelper _linkHelper;

        public FooterViewModel(
            ILinkCollection? linkCollection = null,
            ILinkHelper? linkHelper = null,
            bool useLegacyStyles = false)
        {
            _linkCollection = new LinkCollection();

            _linkCollection = linkCollection ?? new LinkCollection();
            _linkHelper = linkHelper ?? new LinkHelper(_linkCollection);
            UseLegacyStyles = useLegacyStyles;

            AddOrUpdateLink(new BuiltBy(BuiltByHRef, GetLinkClass()));
            AddOrUpdateLink(new OpenGovernmentLicense(OpenGovernmentLicenseHRef, GetLinkClass()));
            AddOrUpdateLink(new OpenGovernmentLicenseV3(OpenGovernmentLicenseHRef, GetLinkClass()));
            AddOrUpdateLink(new CrownCopyright(CrownCopyrightHRef, UseLegacyStyles ? "" : "govuk-footer__link govuk-footer__copyright-logo"));
        }

        private string GetLinkClass()
        {
            return UseLegacyStyles ? "" : "govuk-footer__link";
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
    }
}
