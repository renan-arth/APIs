using System.ComponentModel.DataAnnotations;
using System.Reflection;
using static Platform.Entity.Enum;

namespace Platform.Entity.Utils
{
    public static class EnumHelper
    {
        public static string GetDisplayName(this ActiveStatus status)
        {
            var field = typeof(ActiveStatus).GetField(status.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? status.ToString();
        }

    }
}