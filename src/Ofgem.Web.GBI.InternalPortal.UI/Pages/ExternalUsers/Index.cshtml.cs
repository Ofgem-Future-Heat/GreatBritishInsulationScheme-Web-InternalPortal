using Microsoft.AspNetCore.Mvc.RazorPages;
using Ofgem.Web.GBI.InternalPortal.UI.Services;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.ExternalUsers
{
    public class ExternalUsersModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Remove(ConstantsService.TempDataUserViewModel);
        }
    }
}
