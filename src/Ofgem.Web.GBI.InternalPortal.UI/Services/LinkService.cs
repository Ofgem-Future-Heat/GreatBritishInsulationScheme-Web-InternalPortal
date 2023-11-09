namespace Ofgem.Web.GBI.InternalPortal.UI.Services
{
    public static class LinkService
    {
        public static string CreateMainMenuListItemLink(bool isSelected, string href, string name)
        {
                var underlineClass = isSelected ? "app-navigation__list-item--current" : "";
                var fontColourClass = isSelected ? "govuk-link--no-visited-state" : "";
                var li = $"<li class=\"app-navigation__list-item {underlineClass}\"><a class=\"govuk-link {fontColourClass} govuk-link--no-underline app-navigation__link js-app-navigation__link\" href=\"{href}\">{name}</a></li>";
                return li;
        }
    }
}
