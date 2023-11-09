using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Ofgem.Web.GBI.InternalPortal.Application.Interfaces;
using Ofgem.Web.GBI.InternalPortal.Domain.Models;
using System.Net.Http.Json;

namespace Ofgem.Web.GBI.InternalPortal.Infrastructure.Services
{
    public class UserManagementService : IUserManagementService
    {

        private readonly HttpClient _httpClient;
        private readonly ILogger<UserManagementService> _logger;

        public UserManagementService(HttpClient httpClient, ILogger<UserManagementService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDetailModel>?> GetUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("external-users");
                response.EnsureSuccessStatusCode();

                var responseMessage = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<UserDetailModel>>(responseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUsersAsync method failed to get users.");
                throw;
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.PatchAsync($"external-users/{id}/deactivate", null);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteUserAsync method failed to deactivate user.");
                throw;
            }
        }

        public async Task<UserDetailModel?> GetUserAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"external-users/{id}");
                response.EnsureSuccessStatusCode();

                var responseMessage = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserDetailModel>(responseMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetUserAsync method failed to get user.");
                throw;
            }
        }

        public async Task<Guid> SaveUserAsync(SaveExternalUserRequest user)
        {
            try
            {
                var response = await _httpClient.PutAsync("external-users", JsonContent.Create(user));
                response.EnsureSuccessStatusCode();

                var responseMessage = await response.Content.ReadAsStringAsync();
                var content = JsonConvert.DeserializeObject<Dictionary<string, Guid>>(responseMessage);

                if (content?.TryGetValue("externalUserId", out var result) ?? false)
                {
                    return result;
                }

                Exception exception = new Exception("Invalid response message.");
                throw exception;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SaveUserAsync method failed to save user.");
                throw;
            }
        }
    }
}
