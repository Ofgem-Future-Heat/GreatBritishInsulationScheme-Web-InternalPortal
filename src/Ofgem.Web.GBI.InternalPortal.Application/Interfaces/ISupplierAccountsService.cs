namespace Ofgem.Web.GBI.InternalPortal.Application.Interfaces
{
    public interface ISupplierAccountsService
    {
        Task<IDictionary<int, string>> GetSupplierAccountsAsync();
    }
}
