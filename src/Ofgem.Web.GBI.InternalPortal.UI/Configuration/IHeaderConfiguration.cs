
using System;

namespace Ofgem.GBI.InternalPortal.Service.Configuration
{
    public interface IHeaderConfiguration
    {      
        string? HomeUrl { get; set; }
        string? ExternalUsersUrl { get; set; }        
        string? ClientId { get; set; }
        string? PhaseBannerTag { get; set; }
        string? PhaseBannerFeedbackUrl { get; set; }
    }
}
