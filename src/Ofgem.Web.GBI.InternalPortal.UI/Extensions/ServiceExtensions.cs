using Microsoft.ApplicationInsights.Extensibility;
using Ofgem.Web.GBI.InternalPortal.Application.Interfaces;
using Ofgem.Web.GBI.InternalPortal.Infrastructure.Services;

namespace Ofgem.Web.GBI.InternalPortal.UI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddLogsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITelemetryInitializer, CustomTelemetryInitialiser>();
            services.AddApplicationInsightsTelemetry(configuration.GetSection("APPINSIGHTS_CONNECTIONSTRING"));

            return services;
        }

        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var userManagementUri = new Uri(configuration["UserManagementUrl"]!);
            services.AddHttpClient<IUserManagementService, UserManagementService>(x => x.BaseAddress = userManagementUri);
            services.AddHttpClient<ISupplierAccountsService, SupplierAccountsService>(x => x.BaseAddress = userManagementUri);

			return services;
		}
	}
}
