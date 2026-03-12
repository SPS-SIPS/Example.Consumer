using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SIPS.Example.Consumer.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthAttribute : Attribute, IAsyncAuthorizationFilter
{
    private const string ApiKeyHeaderName = "ApiKey";
    private const string ApiSecretHeaderName = "ApiSecret";

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { error = "API Key was not provided." });
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue(ApiSecretHeaderName, out var extractedApiSecret))
        {
            context.Result = new UnauthorizedObjectResult(new { error = "API Secret was not provided." });
            return;
        }

        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        
        var apiKey = appSettings.GetValue<string>("SIPS:ApiKey");
        var apiSecret = appSettings.GetValue<string>("SIPS:ApiSecret");

        // if keys are defined in configuration, we validate
        if (!string.IsNullOrEmpty(apiKey) && !apiKey.Equals(extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { error = "Unauthorized client." });
            return;
        }

        if (!string.IsNullOrEmpty(apiSecret) && !apiSecret.Equals(extractedApiSecret))
        {
            context.Result = new UnauthorizedObjectResult(new { error = "Unauthorized client." });
            return;
        }

        await Task.CompletedTask;
    }
}
