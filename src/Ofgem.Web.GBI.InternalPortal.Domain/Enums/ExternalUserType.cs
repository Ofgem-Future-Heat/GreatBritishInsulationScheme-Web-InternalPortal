using System.ComponentModel;

namespace Ofgem.Web.GBI.InternalPortal.Domain.Enums
{
    public enum ExternalUserType
    {
        [Description("Authorised Signatory Role")]
        AuthorisedSignatoryRole,
        [Description("Additional User Role")]
        AdditionalUserRole
    }
}
