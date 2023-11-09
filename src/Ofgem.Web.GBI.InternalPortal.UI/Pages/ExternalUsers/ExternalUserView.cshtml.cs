using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ofgem.Web.GBI.InternalPortal.Application.Interfaces;
using Ofgem.Web.GBI.InternalPortal.Domain.Enums;
using Ofgem.Web.GBI.InternalPortal.Domain.Models;
using Ofgem.Web.GBI.InternalPortal.UI.Extensions;
using Ofgem.Web.GBI.InternalPortal.UI.Models;
using Ofgem.Web.GBI.InternalPortal.UI.Services;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.ExternalUsers
{
    public class ExternalUserViewModel : PageModel
    {
        private readonly IUserManagementService _userManagementService;

        public ExternalUserViewModel(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        [BindProperty]
        public UserViewModel? UserViewModel { get; set; } = new UserViewModel();

        public Task<IActionResult> OnPostEdit(string id)
        {
            var result = new RedirectToPageResult(ConstantsService.ExternalUserEditPage, new { id });
            return Task.FromResult((IActionResult)result);
        }

        public async Task<IActionResult> OnPostDelete(Guid id)
        {
            UserDetailModel? user = await _userManagementService.GetUserAsync(id);

            HttpContext.Session.Put(ConstantsService.TempDataUserViewModel, new UserViewModel
            {
                OperationType = OperationType.Delete,
                UserDetail = user!,
                Header = "Confirm that you would like to delete this user"
            });

            return new RedirectToPageResult(ConstantsService.ExternalUserConfirmPage);
        }

        public async Task OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                UserViewModel = null;
            }

            var user = await _userManagementService.GetUserAsync(id);

            if (user == null)
            {
                UserViewModel = null;
            }

            if (UserViewModel != null)
            {
                UserViewModel.UserDetail = user!;
                UserViewModel.Header = "External user details";
            }
        }
    }
}
