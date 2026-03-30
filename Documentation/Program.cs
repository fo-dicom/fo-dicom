// Copyright (c) 2012-2026 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace FO_DICOM.Documentation.DocFx
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Docfx.Dotnet.DotnetApiCatalog.GenerateManagedReferenceYamlFiles("v5/docfx.json");
            await Docfx.Docset.Build("v5/docfx.json");

            await CreateHostBuilder(args).Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
