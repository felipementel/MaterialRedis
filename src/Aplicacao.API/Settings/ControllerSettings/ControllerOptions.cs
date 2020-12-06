using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Aplicacao.API.Settings.SwaggerSettings;
using System.Globalization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Aplicacao.API.Settings.ControllerSettings
{
    public static class ControllerOptions
    {
        public const string CacheProfileName = "NonAuthoritativeLongDatabaseDuration";

        private const string CorsPolicyName = "CorsPolicy";

        public static void AddServiceCORSAplicacao(
            this IServiceCollection services)
        {
            //CORS

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void UseConfigureControllersAplicacao(
            this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            // Extension
            app.UseConfigureSwaggerAplicacao(provider);

            var supportedCultures = new[]
{
                new CultureInfo("pt-BR")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseResponseCompression();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapControllers();

                //HealthCheck
                endpoints
                .MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}