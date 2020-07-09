using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FellowOakDicom.AspNetCore
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddFellowOakDicom(this IServiceCollection services)
            => services.AddFellowOakDicom()
          // .AddLogManager<DicomLogger>()
          ;

        }
}
