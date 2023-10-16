using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eShopAsp.PublicApi.Extensions;

public static class SwaggerGenOptionsExtensions
{
    public static void UseApiEndpoints(this SwaggerGenOptions options)
    {
        options.TagActionsBy(EndpointNamespaceOrDefault);
    }

    private static IList<string?> EndpointNamespaceOrDefault(ApiDescription api)
    {
        if (api.ActionDescriptor is not ControllerActionDescriptor actionDescriptor)
        {
            throw new InvalidOperationException(
                $"Unable determine tag for endpoint: {api.ActionDescriptor.DisplayName}");
        }

        if (actionDescriptor.ControllerTypeInfo.GetBaseTypesAndThis().Any(t => t == typeof(EndpointBase)))
        {
            return new[] { actionDescriptor.ControllerTypeInfo.Namespace?.Split('.').Last() };
        }

        return new[] { actionDescriptor.ControllerName };
    }
}