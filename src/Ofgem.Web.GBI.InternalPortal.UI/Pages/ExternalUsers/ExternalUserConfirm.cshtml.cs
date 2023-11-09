using AutoMapper;
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
    public class ExternalUserConfirmModel : PageModel
    {
        [BindProperty]
        public UserViewModel UserViewModel { get; set; }

        private readonly IUserManagementService _userManagementService;
        private readonly IMapper _mapper;

        public ExternalUserConfirmModel(IUserManagementService userManagementService, IMapper mapper)
        {
            _userManagementService = userManagementService;
            _mapper = mapper;

            UserViewModel = new UserViewModel();
        }

        public IActionResult OnGet()
        {
            UserViewModel = HttpContext.Session.Get<UserViewModel>(ConstantsService.TempDataUserViewModel) ?? new UserViewModel();

            if (UserViewModel.UserDetail == null)
            {
                return BadRequest();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                TempData.Put("UserDetailModel", UserViewModel.UserDetail!);

                if (UserViewModel.OperationType == OperationType.Delete)
                {
                    await _userManagementService.DeleteUserAsync(UserViewModel.UserDetail!.ExternalUserId);
                    HttpContext.Session.Remove(ConstantsService.TempDataUserViewModel);

                    return new RedirectToPageResult(ConstantsService.ExternalUserDeletedPage);
                }
                else
                {
                    var request = _mapper.Map<SaveExternalUserRequest>(UserViewModel.UserDetail);
                    await _userManagementService.SaveUserAsync(request);
                    HttpContext.Session.Remove(ConstantsService.TempDataUserViewModel);

                    return new RedirectToPageResult(ConstantsService.ExternalUserSavedPage, new { UserViewModel.OperationType });
                }
            }
            catch (Exception ex)
            {
                return new RedirectToPageResult(ConstantsService.ExternalUserErrorPage, new { errorMessage = ex.Message });
            }
        }
    }
}
