// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.AspNetCore;
using FellowOakDicom.AspNetCore.Server;
using FO_DICOM.AspNetCoreTest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FO_DICOM.AspNetCoreTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging(c => c.AddConsole());

            services.AddDicomServer<MyDicomService>(o => {
                o.Port = 104;
            });

            services.AddDicomServer(
                o => o.Port = 105,
                builder => builder
                    .AnswerDicomEcho()
                    .CheckAssociationForCalledAET("SERVER")
                    .OnInstanceReceived(e => HandleInstanceReceivedAsync(e))
            );
        }

        private Task<bool> HandleInstanceReceivedAsync(InstanceReceivedEventArgs e)
        {
            // todo: store the file

            return Task.FromResult(true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
