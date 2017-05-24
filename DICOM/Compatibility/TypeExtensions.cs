// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace System.Reflection
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// .NET 3.5 compatibility extension methods for <see cref="Type"/>.
    /// </summary>
    internal static class TypeExtensions
    {
        internal static Type GetTypeInfo(this Type type)
        {
            return type;
        }

        internal static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type, string name)
        {
            return type.GetMethods().Where(m => m.Name.Equals(name));
        }
    }
}