using Microsoft.AspNetCore.Mvc.RazorPages;
using Ofgem.Web.GBI.InternalPortal.Domain.Enums;
using Ofgem.Web.GBI.InternalPortal.Domain.Models;
using Ofgem.Web.GBI.InternalPortal.UI.Extensions;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.ExternalUsers
{
    public class ExternalUserSavedModel : PageModel
    {
        public UserDetailModel UserDetailModel { get; set; } = new UserDetailModel();
        public OperationType OperationType { get; set; }

        public void OnGet(OperationType operationType)
        {
            UserDetailModel = TempData.Get<UserDetailModel>("UserDetailModel") ?? new UserDetailModel();
            OperationType = operationType;
        }
    }
}
