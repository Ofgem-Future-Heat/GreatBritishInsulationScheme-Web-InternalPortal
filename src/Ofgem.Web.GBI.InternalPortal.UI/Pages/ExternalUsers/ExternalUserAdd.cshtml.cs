using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ofgem.Web.GBI.InternalPortal.Application.Interfaces;
using Ofgem.Web.GBI.InternalPortal.UI.Extensions;
using Ofgem.Web.GBI.InternalPortal.UI.Models;
using Ofgem.Web.GBI.InternalPortal.UI.Services;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.ExternalUsers
{
    public class ExternalUserAddModel : PageModel
    {
        private readonly ISupplierAccountsService _supplierAccountsService;

        public ExternalUserAddModel(ISupplierAccountsService supplierAccountsService)
        {
            _supplierAccountsService = supplierAccountsService;
        }

        [BindProperty]
        public UserViewModel? ExternalUserViewModel { get; set; } = new UserViewModel();

        public async Task OnGet()
        {
            if (TempData[ConstantsService.TempDataModelState] != null && TempData[ConstantsService.TempDataUserViewModel] != null)
            {
                ModelState.MergeSerializedTransferValues((string)TempData[ConstantsService.TempDataModelState]!);
                ExternalUserViewModel = TempData.Get<UserViewModel>(ConstantsService.TempDataUserViewModel);
            }
            else
            {
                ExternalUserViewModel = HttpContext.Session.Get<UserViewModel>(ConstantsService.TempDataUserViewModel) ?? new UserViewModel();
            }

            await InitAsync();
        }

        public async Task<IActionResult> OnPostAsync(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ConstantsService.TempDataModelState] = ModelState.Serialize();
                TempData.Put(ConstantsService.TempDataUserViewModel, model);

                return new RedirectToPageResult("ExternalUserAdd");
            }

            var suppliers = await _supplierAccountsService.GetSupplierAccountsAsync();
            ExternalUserViewModel!.UserDetail = model.UserDetail;
            ExternalUserViewModel.Suppliers = suppliers;
            ExternalUserViewModel.PersonalDetailsTableCaption = ConstantsService.PersonalDetailsTableTitle;
            ExternalUserViewModel.Header = "Check your answers before adding this user";
            if (ExternalUserViewModel.UserDetail != null)
            {
                ExternalUserViewModel.UserDetail.Supplier.SupplierName = model.UserDetail!.SupplierId.HasValue ? suppliers[model.UserDetail.SupplierId.Value] : null;
            }

            HttpContext.Session.Put(ConstantsService.TempDataUserViewModel, ExternalUserViewModel);

            return new RedirectToPageResult(ConstantsService.ExternalUserConfirmPage);
        }

        private async Task InitAsync()
        {
            ExternalUserViewModel!.Header = "Create a new external user";
            ExternalUserViewModel!.Suppliers = await _supplierAccountsService.GetSupplierAccountsAsync();
        }
    }
}
