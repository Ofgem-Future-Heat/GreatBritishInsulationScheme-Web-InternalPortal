using Microsoft.AspNetCore.Mvc;
using Ofgem.Web.GBI.InternalPortal.Domain.Enums;
using Ofgem.Web.GBI.InternalPortal.Domain.Models;

namespace Ofgem.Web.GBI.InternalPortal.UI.Models
{
    public class UserViewModel
    {
        public UserDetailModel? UserDetail { get; set; }
        public IDictionary<int, string>? Suppliers { get; set; }
        public string? Header { get; set; }
        public OperationType OperationType { get; set; }
        public string? PersonalDetailsTableCaption { get; set; }
    }
}
