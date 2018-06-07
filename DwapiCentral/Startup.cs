using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Infrastructure.Data;
using DwapiCentral.Cbs.Infrastructure.Data.Repository;
using DwapiCentral.SharedKernel.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using StructureMap;

namespace DwapiCentral
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public static IServiceProvider ServiceProvider { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assemblyNames = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var assemblyName in assemblyNames)
            {
                assemblies.Add(Assembly.Load(assemblyName));
            }
            services.AddMediatR(assemblies);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Configuration["ConnectionStrings:DwapiConnection"];
            services.AddDbContext<CbsContext>(o => o.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(CbsContext).GetTypeInfo().Assembly.GetName().Name)));

            services.AddScoped<IMasterFacilityRepository, MasterFacilityRepository>();
            
            var container = new Container();
            container.Populate(services);
            ServiceProvider = container.GetInstance<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            EnsureMigrationOfContext<CbsContext>();

            Log.Debug(@"initializing Database [Complete]");
            Log.Debug(
                @"---------------------------------------------------------------------------------------------------");
            Log.Debug(@"

                        ________                        .__    _________                __                .__   
                        \______ \__  _  _______  ______ |__|   \_   ___ \  ____   _____/  |_____________  |  |  
                         |    |  \ \/ \/ /\__  \ \____ \|  |   /    \  \/_/ __ \ /    \   __\_  __ \__  \ |  |  
                         |    `   \     /  / __ \|  |_> >  |   \     \___\  ___/|   |  \  |  |  | \// __ \|  |__
                        /_______  /\/\_/  (____  /   __/|__| /\ \______  /\___  >___|  /__|  |__|  (____  /____/
                                \/             \/|__|        \/        \/     \/     \/                 \/      

            ");
            Log.Debug(
                @"---------------------------------------------------------------------------------------------------");
            Log.Debug("Dwapi Central started !");
        }

        public static void EnsureMigrationOfContext<T>() where T : BaseContext
        {
            var contextName = typeof(T).Name;
            Log.Debug($"initializing Database context: {contextName}");
            var context = ServiceProvider.GetService<T>();
            try
            {
                context.Database.Migrate();
                context.EnsureSeeded();
                Log.Debug($"initializing Database context: {contextName} [OK]");
            }
            catch (Exception e)
            {
                Log.Debug($"initializing Database context: {contextName} Error");
                Log.Debug($"{e}");
            }
        }

    }
}
