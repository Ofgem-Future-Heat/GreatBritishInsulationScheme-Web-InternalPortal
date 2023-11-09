using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ofgem.Web.GBI.InternalPortal.Application.Interfaces;
using Ofgem.Web.GBI.InternalPortal.Domain.Enums;
using Ofgem.Web.GBI.InternalPortal.UI.Extensions;
using Ofgem.Web.GBI.InternalPortal.UI.Models;
using Ofgem.Web.GBI.InternalPortal.UI.Services;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.ExternalUsers
{
    public class ExternalUserEditModel : PageModel
    {
        private readonly IUserManagementService _userManagementService;
        private readonly ISupplierAccountsService _supplierAccountService;

        public ExternalUserEditModel(IUserManagementService userManagementService, ISupplierAccountsService supplierAccountService)
        {
            _userManagementService = userManagementService;
            _supplierAccountService = supplierAccountService;
        }

        public UserViewModel? ExternalUserViewModel { get; set; } = new UserViewModel();

        public async Task OnGet(Guid id)
        {
            if (TempData[ConstantsService.TempDataModelState] != null && TempData[ConstantsService.TempDataUserViewModel] != null)
            {
                ModelState.MergeSerializedTransferValues((string)TempData[ConstantsService.TempDataModelState]!);
                ExternalUserViewModel = TempData.Get<UserViewModel>(ConstantsService.TempDataUserViewModel);
            }
            else if (id != Guid.Empty)
            {
                var user = await _userManagementService.GetUserAsync(id);

                if (user == null)
                {
                    ExternalUserViewModel = null;
                }

                if (ExternalUserViewModel != null)
                {
                    ExternalUserViewModel.UserDetail = user!;
                }
            }
            else
            {
                ExternalUserViewModel = HttpContext.Session.Get<UserViewModel>(ConstantsService.TempDataUserViewModel);
            }

            if (ExternalUserViewModel != null)
            {
                await InitAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ConstantsService.TempDataModelState] = ModelState.Serialize();
                TempData.Put(ConstantsService.TempDataUserViewModel, model);

                return new RedirectToPageResult("ExternalUserEdit");
            }

            var suppliers = await _supplierAccountService.GetSupplierAccountsAsync();

            if (ExternalUserViewModel != null)
            {
                ExternalUserViewModel.UserDetail = model.UserDetail;
                ExternalUserViewModel.Suppliers = suppliers;
                ExternalUserViewModel.OperationType = OperationType.Update;
                ExternalUserViewModel.Header = "Check your answers before editing this user's details";
                ExternalUserViewModel.PersonalDetailsTableCaption = ConstantsService.PersonalDetailsTableTitle;

                if (ExternalUserViewModel.UserDetail != null)
                {
					ExternalUserViewModel.UserDetail.Supplier.SupplierName = model.UserDetail!.SupplierId.HasValue ? suppliers[model.UserDetail.SupplierId.Value] : null;
				}

                HttpContext.Session.Put(ConstantsService.TempDataUserViewModel, ExternalUserViewModel);
            }

            return new RedirectToPageResult(ConstantsService.ExternalUserConfirmPage);
        }

        public async Task InitAsync()
        {
            ExternalUserViewModel!.Header = "Edit external user";
            ExternalUserViewModel.Suppliers = await _supplierAccountService.GetSupplierAccountsAsync();
        }
    }
}
