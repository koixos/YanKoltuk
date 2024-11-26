using System.ComponentModel;
using System.Reflection;

namespace YanKoltukBackend.Shared
{
    public static class Extensions
    {
        public static string GetDescription(this Enum e)
        {
            var attribute =
                e.GetType()
                    .GetTypeInfo()
                    .GetMember(e.ToString())
                    .FirstOrDefault(m => m.MemberType == MemberTypes.Field)?
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .SingleOrDefault()
                    as DescriptionAttribute;
            return attribute?.Description ?? e.ToString();
        }
    }
}