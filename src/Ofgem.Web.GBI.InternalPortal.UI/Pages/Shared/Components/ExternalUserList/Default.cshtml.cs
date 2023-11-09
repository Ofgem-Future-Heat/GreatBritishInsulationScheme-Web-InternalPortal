using Microsoft.AspNetCore.Mvc;
using Ofgem.Web.GBI.InternalPortal.Application.Interfaces;
using Ofgem.Web.GBI.InternalPortal.Domain.Models;

namespace Ofgem.Web.GBI.InternalPortal.UI.Pages.Shared.Components.ExternalUserList
{
    public class ExternalUserList : ViewComponent
    {
        private readonly IUserManagementService _userManagementService;

        public ExternalUserList(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = await _userManagementService.GetUsersAsync();
            
            return (IViewComponentResult)View(users);
        }
    }
}
