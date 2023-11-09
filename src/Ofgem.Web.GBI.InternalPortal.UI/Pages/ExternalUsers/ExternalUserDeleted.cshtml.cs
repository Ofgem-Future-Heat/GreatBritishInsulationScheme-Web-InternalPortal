using Microsoft.AspNetCore.Mvc.RazorPages;
using Ofgem.Web.GBI.InternalPortal.Domain.Models;
using Ofgem.Web.GBI.InternalPortal.UI.Extensions;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.ExternalUsers
{
    public class ExternalUserDeletedModel : PageModel
    {
        public UserDetailModel UserDetailModel { get; set; } = new UserDetailModel();

        public void OnGet()
        {
            UserDetailModel = TempData.Get<UserDetailModel>("UserDetailModel") ?? new UserDetailModel();
        }
    }
}
