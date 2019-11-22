// Copyright (c) 2012-2019 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FellowOakDicom
{

    public class DicomSetupBuilder
    {
        private readonly IServiceCollection _serviceCollection;

        public DicomSetupBuilder()
        {
            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddDefaultDicomServices();
        }

        public void Build()
        {
            var provider = _serviceCollection.BuildServiceProvider();
            Setup.SetupDI(provider);
        }

        public DicomSetupBuilder RegisterServices(Action<IServiceCollection> registerAction)
        {
            registerAction?.Invoke(_serviceCollection);
            return this;
        }

    }

    /// <summary>
    /// Setup helper methods for initializing library.
    /// </summary>
    internal static class Setup
    {

        private static IServiceProvider _serviceProvider;
        internal static IServiceProvider ServiceProvider
        { 
            get
            {
                if (_serviceProvider == null)
                {
                    new DicomSetupBuilder().Build();
                }
                return _serviceProvider;
            }
            private set => _serviceProvider = value;
        }

        public static void SetupDI(IServiceProvider diProvider)
        {
            ServiceProvider = diProvider;
        }


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

        private static IEnumerable<Assembly> GetPlatformAssemblies()
        {
            return new[] { typeof(Setup).GetTypeInfo().Assembly };
        }
    }
}
