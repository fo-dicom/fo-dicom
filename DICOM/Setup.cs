// Copyright (c) 2012-2016 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Setup helper methods for initializing library.
    /// </summary>
    internal static class Setup
    {
        #region FIELDS

        /// <summary>
        /// Name of the fo-dicom platform-specific assembly.
        /// </summary>
        private const string platformAssemblyName = "Dicom.Platform";

        #endregion

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
                var assembly = Assembly.Load(new AssemblyName(platformAssemblyName));
#if NET35
                var type = assembly.GetTypes().Single(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract);
                var instance = (T)Activator.CreateInstance(type);
#else
                var type = assembly.DefinedTypes.Single(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract);
                var instance = (T)Activator.CreateInstance(type.AsType());
#endif

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
                var assembly = Assembly.Load(new AssemblyName(platformAssemblyName));
#if NET35
                var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract);
                var instance = types.Select(t => (T)Activator.CreateInstance(t)).Single(obj => obj.IsDefault);
#else
                var types = assembly.DefinedTypes.Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract);
                var instance = types.Select(t => (T)Activator.CreateInstance(t.AsType())).Single(obj => obj.IsDefault);
#endif
                return instance;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}