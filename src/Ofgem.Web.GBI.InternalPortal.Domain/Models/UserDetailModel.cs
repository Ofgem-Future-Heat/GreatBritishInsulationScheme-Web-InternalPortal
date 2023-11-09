using Ofgem.Web.GBI.InternalPortal.Domain.Constants;
using Ofgem.Web.GBI.InternalPortal.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ofgem.Web.GBI.InternalPortal.Domain.Models
{
    public class UserDetailModel
    {
        /// <summary>
        /// Unique Id for your user
        /// This field also indicates whether the user detail is new or an existing one
        /// i.e. string.Empty = new user, populated value = existing user
        /// </summary>
        public Guid ExternalUserId { get; set; }

        [Required(ErrorMessage = "First name cannot be empty")]
        [StringLength(50, ErrorMessage = "First name has too many characters (max 50)")]
        [RegularExpression(RegularExpressions.UserFirstLastName, ErrorMessage = "First name must only include letters a to z, numbers, spaces and special characters of space, hyphen and apostrophe")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name cannot be empty")]
        [StringLength(50, ErrorMessage = "Last name has too many characters (max 50)")]
        [RegularExpression(RegularExpressions.UserFirstLastName, ErrorMessage = "Last name must only include letters a to z, numbers, spaces and special characters of space, hyphen and apostrophe")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email address cannot be empty")]
        [MinLength(5, ErrorMessage = "Too few characters (min 5)")]
        [StringLength(250, ErrorMessage = "Email address has too many characters (max 250)")]
        [RegularExpression(RegularExpressions.EmailAddress, ErrorMessage = "You must enter a valid email address")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "You must select a user type")]
        public ExternalUserType? UserType { get; set; }

        public bool? IsActive { get; set; }

        public int? SupplierId { get { return Supplier.SupplierId; } }
        public string Status { get { return IsActive.HasValue && IsActive.Value ? "Active" : "Inactive"; } }

        public SupplierModel Supplier { get; set; } = new SupplierModel();
    }
}