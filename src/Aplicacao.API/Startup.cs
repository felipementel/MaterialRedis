using Aplicacao.API.Settings.ControllerSettings;
using Aplicacao.API.Settings.SwaggerSettings;
using Aplicacao.Infra.CrossCutting;
using Aplicacao.Infra.DataAccess.Context;
using AutoMapper;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace Aplicacao.API
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public IWebHostEnvironment _environment { get; }

        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(opt =>
                {
                    var serializerOptions = opt.JsonSerializerOptions;
                    serializerOptions.IgnoreNullValues = true;
                    serializerOptions.IgnoreReadOnlyProperties = false;
                    serializerOptions.WriteIndented = true;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.IgnoreNullValues = false;
                    options.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new BadRequestObjectResult(context.ModelState);

                        result.ContentTypes.Add(MediaTypeNames.Application.Json);
                        result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                        return result;
                    };
                });
                


            if (_environment.IsDevelopment())
            {
                services.AddControllers(opts =>
                {
                    opts.Filters.Add(new AllowAnonymousFilter());
                });
            }

            services
                .Configure<GzipCompressionProviderOptions>(options =>
                    options.Level = System.IO.Compression.CompressionLevel.Optimal)
                .AddResponseCompression(options =>
                {
                    options.Providers.Add<GzipCompressionProvider>();
                    options.EnableForHttps = true;
                });


            services
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConfiguration(_configuration.GetSection("Logging"));
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                });

            services
                .AddDbContext<DbContext, AplicacaoContext>(opt => opt
                .UseSqlServer(_configuration.GetConnectionString("AzureDatabase")
                , options =>
                {
                    options
                    //.EnableRetryOnFailure(
                    //    maxRetryCount: 3,
                    //    maxRetryDelay: TimeSpan.FromSeconds(4),
                    //    errorNumbersToAdd: null)
                    .MigrationsHistoryTable("MigracoesEFAplicacao", "dbo");
                }
                )
                .EnableSensitiveDataLogging(_environment.IsDevelopment())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll));


            services
                .AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = _configuration.GetConnectionString("Redis");
                    options.InstanceName = "Aplicacao-";
                });

            //https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1
            services
                .AddOptions();

            services
                .AddAutoMapper(typeof(Startup));

            // Extension
            services
                .AddSwaggerConfigAplicacao(_environment);

            services
                .AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                    //options.ApiVersionReader = new HeaderApiVersionReader("x-api-gavea");
                })
            .AddVersionedApiExplorer(options =>
            {
                //The format of the version added to the route URL  
                options.GroupNameFormat = "'v'VVV";
                //Tells swagger to replace the version in the controller route  
                options.SubstituteApiVersionInUrl = true;
            });

            // Extension
            services
                .AddServiceCORSAplicacao();

            // Extension
            services
                .AddRegisterServicesAplicacao();

            Injections
                .SetSecurity(services, _configuration);

            services
                .AddApplicationInsightsTelemetry(opt =>
                {
                    opt.InstrumentationKey = _configuration.GetValue("ApplicationInsights:InstrumentationKey", string.Empty);
                });

            services
                .AddHealthChecks();

            services
                .ConfigureTelemetryModule<QuickPulseTelemetryModule>((module, o) =>
                {
                    module.AuthenticationApiKey = _configuration.GetValue("ApplicationInsights:AuthenticationApiKey", string.Empty);
                });

            services
                .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
                {
                    module.EnableSqlCommandTextInstrumentation = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider,
            IHostApplicationLifetime lifetime,
            IDistributedCache cache,
            DbContext dataContext)
        {
            //dataContext.Database.EnsureCreated();
            //dataContext.Database.Migrate();

            lifetime.ApplicationStarted.Register(() =>
            {
                var currentTimeUTC = DateTime.UtcNow.ToString();
                byte[] encodedCurrentTimeUTC = Encoding.UTF8.GetBytes(currentTimeUTC);

                var options = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                cache.Set("cachedTimeUTC", encodedCurrentTimeUTC, options);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                context.Response.ContentType = "text/html";
                                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                                if (null != exceptionObject)
                                {
                                    var errorMessage = $"<b>Error: {exceptionObject.Error.Message}</b> { exceptionObject.Error.StackTrace}";
                                    await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                                }
                            });
                    }
                    );
            }

            // Extension
            app.UseConfigureControllersAplicacao(provider);
        }
    }
}
