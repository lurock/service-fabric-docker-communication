using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ReliableClientService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                try
                {
                    Uri uri = new Uri("http://dockerserver.test-application:5000/api/values");
                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync(uri);
                    var result = await response.Content.ReadAsStringAsync();
                    await context.Response.WriteAsync("Reliable client response from server: " + result);
                }
                catch (Exception e)
                {
                    await context.Response.WriteAsync("Reliable client failed to get endpoint.  Exception occurred: " + e.ToString());
                }
            });
        }
    }
}
