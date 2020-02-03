using System.Reflection;
using System.Runtime.Versioning;
using Xunit;

namespace FellowOakDicom.Tests.Helpers
{
    public class IgnoreOnDesktopFactAttribute : FactAttribute
    {

        public IgnoreOnDesktopFactAttribute()
        {
            if (IsRunningOnNetFramework)
            {
                Skip = "Ignored on NetFramework";
            }
        }

        public static bool IsRunningOnNetFramework
            => Assembly
            .GetEntryAssembly()?
            .GetCustomAttribute<TargetFrameworkAttribute>()?
            .FrameworkName == "net462";
    }
}
