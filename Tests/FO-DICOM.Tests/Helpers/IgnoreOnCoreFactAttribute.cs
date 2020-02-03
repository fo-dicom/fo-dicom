using System.Reflection;
using System.Runtime.Versioning;
using Xunit;

namespace FellowOakDicom.Tests.Helpers
{
    class IgnoreOnCoreFactAttribute : FactAttribute
    {
        public IgnoreOnCoreFactAttribute()
        {
            if (IsRunningOnNetCore)
            {
                Skip = "Ignored on NetCore";
            }
        }

        public static bool IsRunningOnNetCore
            => Assembly
            .GetEntryAssembly()?
            .GetCustomAttribute<TargetFrameworkAttribute>()?
            .FrameworkName.StartsWith("netcore") ?? false;

    }
}
