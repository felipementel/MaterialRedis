using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Aplicacao.API.Settings.SwaggerSettings
{
    public static class SwaggerConfigs
    {
        public static void AddSwaggerConfigAplicacao(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Demo - Coders in Rio </>",
                    Version = "v1",
                    Description = $"Parameters: </br>" +
                    $" - EnvironmentName: {environment.EnvironmentName} </br>" +
                    $" - AssemblyVersion: {Assembly.GetEntryAssembly().GetName().Version} </br>" +
                    $" - OSVersion: {Environment.OSVersion} </br>" +
                    $" - ServerDateAndHour: {DateTime.Now:u} </br>",
                    TermsOfService = new Uri("https://felipementel.dev.br"),
                    Contact = new OpenApiContact
                    {
                        Name = "Felipe",
                        Email = "admin@felipementel.dev.br"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "BSD",
                        Url = new Uri("https://pt.wikipedia.org/wiki/Licen%C3%A7a_BSD"),
                    }
                });

                //Caso exista alguma api com v2, crie um documento similar ao acima,
                // alterando a versao, de v1 para v2

                options.OperationFilter<CustomHeaderOperationFilter>();
                options.DocumentFilter<CustomSwaggerDocumentFilter>();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header | Bearer Schema. <br>
                      Informe: Bearer [espaço] o token obtido na controller de login <br>
                      Exemplo: Bearer 12345abcdef",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.CustomSchemaIds(i => i.FullName);
            });
        }

        /// <summary>
        /// Configura o Swagger
        /// </summary>
        /// <param name="app">Aplicação</param>
        /// <param name="provider"></param>
        public static void UseConfigureSwaggerAplicacao(
            this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                options.DisplayRequestDuration();
                options.DisplayOperationId();
                options.EnableDeepLinking();
                options.EnableValidator();
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                
                //Build a swagger endpoint for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}