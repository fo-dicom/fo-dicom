// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FO_DICOM.AspNetCoreTest
{
    public class Program
   {
      public static void Main(string[] args)
      {
         CreateHostBuilder(args).Build().Run();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
              });
   }
}
