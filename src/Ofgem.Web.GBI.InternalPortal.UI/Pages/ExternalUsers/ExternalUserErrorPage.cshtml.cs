using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.ExternalUsers
{
    public class ExternalUserErrorPageModel : PageModel
    {
        public string? ErrorMessage { get; set; }

        public void OnGet(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
