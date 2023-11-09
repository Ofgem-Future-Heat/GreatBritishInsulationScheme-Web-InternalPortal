using Ofgem.Web.GBI.InternalPortal.Domain.Models;

namespace Ofgem.Web.GBI.InternalPortal.Application.Interfaces
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserDetailModel>?> GetUsersAsync();
        Task<UserDetailModel?> GetUserAsync(Guid id);
        Task<Guid> SaveUserAsync(SaveExternalUserRequest user);
        Task DeleteUserAsync(Guid id);
    }
}