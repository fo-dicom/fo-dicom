// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FellowOakDicom
{

    /// <summary>
    /// Setup helper methods for initializing library.
    /// </summary>
    internal static class Setup
    {
        /// <summary>
        /// Gets a single instance from the platform assembly implementing the <typeparamref name="T"/> type.
        /// </summary>
        /// <typeparam name="T">(Abstract) type for which implementation is requested.</typeparam>
        /// <returns>The single instance from the platform assembly implementing the <typeparamref name="T"/> type, 
        /// or null if no or more than one implementations are available.</returns>
        /// <remarks>It is implicitly assumed that implementation class has a public, parameterless constructor.</remarks>
        internal static T GetSinglePlatformInstance<T>() where T : class
        {
            try
            {
                var assemblies = GetPlatformAssemblies();
                var type =
                    assemblies.SelectMany(assembly => assembly.DefinedTypes)
                        .Single(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract);
                var instance = (T)Activator.CreateInstance(type.AsType());

                return instance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the default classified instance from the platform assembly implementing the <typeparamref name="T"/> type.
        /// </summary>
        /// <typeparam name="T">(Abstract) type for which implementation is requested.</typeparam>
        /// <returns>The single instance from the platform assembly implementing the <typeparamref name="T"/> type that is classified as default, 
        /// or null if no or more than one default classified implementations are available.</returns>
        /// <remarks>It is implicitly assumed that all implementation classes has a public, parameterless constructor.</remarks>
        internal static T GetDefaultPlatformInstance<T>() where T : class, IClassifiedManager
        {
            try
            {
                var assemblies = GetPlatformAssemblies();
                var types =
                    assemblies.SelectMany(assembly => assembly.DefinedTypes)
                        .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract);
                var instance = types.Select(t => (T)Activator.CreateInstance(t.AsType())).Single(obj => obj.IsDefault);
                return instance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static IEnumerable<Assembly> GetPlatformAssemblies()
        {
            return new[] { typeof(Setup).GetTypeInfo().Assembly };
        }
    }
}
