using Microsoft.AspNetCore.Mvc;
using Ofgem.Web.GBI.InternalPortal.UI.Models;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.Shared.Components.EditUser
{
    public class EditUser : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(UserViewModel userViewModel)
        {
            IViewComponentResult result = View(userViewModel ?? new UserViewModel());

            return Task.FromResult(result);
        }
    }
}
