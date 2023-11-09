using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.Errors
{
    public class ForbiddenModel : PageModel
    {
        public void OnGet()
        {
            Response.StatusCode = 403;
        }
    }
}
