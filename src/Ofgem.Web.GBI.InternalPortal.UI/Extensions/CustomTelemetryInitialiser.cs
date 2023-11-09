using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Ofgem.Web.GBI.InternalPortal.UI.Extensions
{
    public class CustomTelemetryInitialiser : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry == null) return;
            telemetry.Context.Cloud.RoleName = "GBI-InternalPortal-Web";
        }
    }
}
