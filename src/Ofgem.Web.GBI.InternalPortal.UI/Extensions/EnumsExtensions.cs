
using System.ComponentModel;

namespace Ofgem.Web.GBI.InternalPortal.UI.Extensions
{
    public static class EnumsExtensions
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());

            if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            throw new ArgumentException("Item not found.", nameof(enumValue));
        }
    }
}
