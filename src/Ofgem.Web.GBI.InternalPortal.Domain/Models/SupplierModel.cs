using System.ComponentModel.DataAnnotations;

namespace Ofgem.Web.GBI.InternalPortal.Domain.Models
{
    public class SupplierModel
    {
        [Required(ErrorMessage = "You must select an account")]
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
    }
}
