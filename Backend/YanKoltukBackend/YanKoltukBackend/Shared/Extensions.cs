using System.ComponentModel;
using System.Reflection;
using YanKoltukBackend.Models.Entities;

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

        public static StudentStatus GetStudentStatus(this string s)
        {
            if (s.Equals("GetOff"))
                return StudentStatus.GetOff;
            if (s.Equals("GetOn"))
                return StudentStatus.GetOn;
            return StudentStatus.Error;
        }

        public static TripType GetTripType(this string s)
        {
            if (s.Equals("ToSchool"))
                return TripType.ToSchool;
            if (s.Equals("FromSchool"))
                return TripType.FromSchool;
            return TripType.Error;
        }
    }
}