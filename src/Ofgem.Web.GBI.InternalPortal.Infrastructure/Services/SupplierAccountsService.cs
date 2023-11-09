using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ofgem.Web.GBI.InternalPortal.Application.Interfaces;
using Ofgem.Web.GBI.InternalPortal.Domain.Models;
using System.Linq;

namespace Ofgem.Web.GBI.InternalPortal.Infrastructure.Services
{
    public class SupplierAccountsService : ISupplierAccountsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SupplierAccountsService> _logger;

        public SupplierAccountsService(HttpClient httpClient, ILogger<SupplierAccountsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IDictionary<int, string>> GetSupplierAccountsAsync()
        {
            var suppliers = new Dictionary<int, string>();

            try
            {
                var response = await _httpClient.GetAsync("suppliers");
                response.EnsureSuccessStatusCode();

                var responseMessage = await response.Content.ReadAsStringAsync();
                var suppliersList = JsonConvert.DeserializeObject<IEnumerable<SupplierModel>>(responseMessage);

                suppliersList = suppliersList?.Where(s => s.SupplierId.HasValue).OrderBy(c=>c.SupplierName);
                suppliers = suppliersList?.ToDictionary(c => c.SupplierId!.Value, c => (c.SupplierName??string.Empty));

                return suppliers!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetSupplierAccountsAsync method failed to get suppliers: {Message}", ex.Message);
                throw;
            }
        }
    }
}
