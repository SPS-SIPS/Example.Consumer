using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SIPS.Example.Consumer.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthAttribute : Attribute, IAsyncAuthorizationFilter
{
    private const string ApiKeyHeaderName = "API_Key";
    private const string ApiSecretHeaderName = "API_Secret";

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("API Key was not provided.");
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue(ApiSecretHeaderName, out var extractedApiSecret))
        {
            context.Result = new UnauthorizedObjectResult("API Secret was not provided.");
            return;
        }

        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        
        var apiKey = appSettings.GetValue<string>("SIPS:ApiKey");
        var apiSecret = appSettings.GetValue<string>("SIPS:ApiSecret");

        // if keys are defined in configuration, we validate
        if (!string.IsNullOrEmpty(apiKey) && !apiKey.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized client.");
            return;
        }

        if (!string.IsNullOrEmpty(apiSecret) && !apiSecret.Equals(extractedApiSecret))
        {
            context.Result = new UnauthorizedObjectResult("Unauthorized client.");
            return;
        }

        await Task.CompletedTask;
    }
}
