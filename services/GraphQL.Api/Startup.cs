using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NHLStats.Core.Data;
using NHLStats.Data;
using NHLStats.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

using GraphQL.Api.Models;
using GraphQL;
using GraphQL.Types;
using GraphQLApi.Ui.Playground;
using Hangfire;
using NHLStats.Core;
using NHLStats.Data.Hubs;

namespace GraphQL.Api
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
			services.AddDbContext<NHLStatsContext>(options => 
			                                       options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddTransient<IPlayerRepository, PlayerRepository>();
			services.AddTransient<ISkaterStatisticRepository, SkaterStatisticRepository>();
            services.AddTransient<IProcessor, Processor>();
            // GraphQL
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<NHLStatsQuery>();
            services.AddSingleton<NHLStatsMutation>();
            services.AddSingleton<PlayerType>();
            services.AddSingleton<PlayerInputType>();
            services.AddSingleton<SkaterStatisticInputType>();
            services.AddSingleton<SkaterStatisticType>();
            services.AddSingleton<LeagueType>();
            services.AddSingleton<TeamType>();
            services.AddSingleton<SeasonType>();
            services.AddSingleton<StatusResultType>();
            services.AddSingleton<StatusTypeEnum>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new NHLStatsSchema(new FuncDependencyResolver(type => sp.GetService(type))));


            // GlobalConfiguration.Configuration.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"));
                services.AddHangfire(configuration =>
                    {
                        // Do pretty much the same as you'd do with 
                        // GlobalConfiguration.Configuration in classic .NET

                        // NOTE: logger and activator would be configured automatically, 
                        // and in most cases you don't need to configure those.

                        configuration.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"));

                        // ... maybe something else, e.g. configuration.UseConsole()
                    });

            services.AddMvc();

            //SignalR and Azure
            services.AddSignalR().AddAzureSignalR();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "clientApp/build";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, NHLStatsContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();


            var playGround = "/ui/playground";
            var options = new DashboardOptions { AppPath = playGround };

            app.UseHangfireDashboard("/hangfire", options);
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()
            {
                Path = playGround
            });
            app.UseGraphiQl();


            // app.UseMvc();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            // Azure SignalR
            app.UseAzureSignalR(routes => {
                routes.MapHub<NotificationHub>("/notifications");
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "clientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

			db.EnsureSeedData();
        }
    }
}
