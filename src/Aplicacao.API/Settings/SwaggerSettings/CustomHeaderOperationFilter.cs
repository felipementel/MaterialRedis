using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Aplicacao.API.Settings.SwaggerSettings
{
    public class CustomHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            //operation.Parameters.Add(new OpenApiParameter
            //{
            //    Name = "x-customHeader",
            //    In = ParameterLocation.Header,
            //    Required = true,
            //    Schema = new OpenApiSchema
            //    {
            //        Type = "String"
            //    }
            //});
        }
    }
}