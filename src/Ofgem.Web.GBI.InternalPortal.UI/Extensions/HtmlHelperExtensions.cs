using Microsoft.AspNetCore.Mvc.Rendering;
using Ofgem.GBI.InternalPortal.Service.Configuration;
using Ofgem.GBI.InternalPortal.Service.Models;

namespace Ofgem.GBI.InternalPortal.Web.Extensions;

public static class HtmlHelperExtensions
    {
        public static IHeaderViewModel GetHeaderViewModel(this IHtmlHelper html)
    {

        var headerModel = new HeaderViewModel(new HeaderConfiguration
        {
            HomeUrl = "/Index",
            ExternalUsersUrl = "/ExternalUsers",
            PhaseBannerTag = "Beta",
            PhaseBannerFeedbackUrl = "#",
        },
        new UserContext
        {
            User = html?.ViewContext.HttpContext.User,
            //HashedAccountId = html.ViewContext.RouteData.Values["employerAccountId"]?.ToString()
        },
        currentUrl: html!.ViewContext?.HttpContext?.Request?.Path.Value);

        if (html.ViewBag.HideNav is bool && html.ViewBag.HideNav)
        {
            headerModel.HideMenu();
        }

        return headerModel;
    }

    public static IFooterViewModel GetFooterViewModel(this IHtmlHelper html)
        {
            return new FooterViewModel();
        }
    }