#include "../SharedAssemblyInfo.cs"

; // terminate cs "include"

using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Runtime::InteropServices;
using namespace System::Security::Permissions;

[assembly: AssemblyTitleAttribute("DICOM for .NET Native")];
[assembly: AssemblyDescriptionAttribute("")];
[assembly: AssemblyConfigurationAttribute("")];

[assembly: ComVisible(false)];
[assembly: CLSCompliantAttribute(true)];
[assembly: SecurityPermission(SecurityAction::RequestMinimum, UnmanagedCode = true)];
