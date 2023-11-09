using Ofgem.Web.GBI.InternalPortal.Domain.Enums;

namespace Ofgem.Web.GBI.InternalPortal.Domain.Models
{
	public class SaveExternalUserRequest
	{
		public Guid ExternalUserId { get; set; }
		public int SupplierId { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? EmailAddress { get; set; }
		public ExternalUserType UserType { get; set; }
	}
}
