using System.Text.Json;

namespace Ofgem.Web.GBI.InternalPortal.UI.Extensions
{
    public static class SessionExtensions
    {
        public static void Put<T>(this ISession session, string key, T value) where T : class
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key) where T : class
        {
            string? value = session.GetString(key);

            return value != null ? JsonSerializer.Deserialize<T>(value) : null;
        }
    }
}
