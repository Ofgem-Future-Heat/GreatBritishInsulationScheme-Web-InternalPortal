using System;

namespace Ofgem.GBI.InternalPortal.Service.Configuration
{
    public class HeaderConfiguration : IHeaderConfiguration
    {
        public string? ApplicationBaseUrl { get; set; }
        public string? EmployerCommitmentsV2BaseUrl { get; set; }
        public string? HomeUrl { get; set; }
        public string? ExternalUsersUrl { get; set; }        
        public string? AuthenticationAuthorityUrl { get; set; }
        public string? ClientId { get; set; }
        public string? EmployerRecruitBaseUrl { get; set; }
        public Uri? SignOutUrl { get; set; }
        public Uri? ChangeEmailReturnUrl { get; set; }
        public Uri? ChangePasswordReturnUrl { get; set; }
        public string? PhaseBannerTag { get; set; }
        public string? PhaseBannerFeedbackUrl { get; set;}
    }
}
