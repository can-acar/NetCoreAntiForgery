using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace Lib
{
    public class ApiAntiForgeryFilter : IAsyncAuthorizationFilter, IAntiforgeryPolicy, IFilterMetadata
    {
        private readonly IAntiforgery Antiforgery;
        private readonly ILogger Logger;

        public ApiAntiForgeryFilter(IAntiforgery antiforgery, ILoggerFactory logger)
        {
            Antiforgery = antiforgery ?? throw new ArgumentNullException(nameof(antiforgery));
            Logger = logger.CreateLogger(typeof(ApiAntiForgeryFilter));
            ;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ApiAntiForgeryFilter authorizationFilter = this;
            if (context == null)
                throw new ArgumentNullException(nameof(context));


            if (!context.IsEffectivePolicy((IAntiforgeryPolicy) authorizationFilter))
            {
                authorizationFilter.Logger.Log(LogLevel.Trace, new EventId(1, "NotMostEffectiveFilter"),
                    $"Skipping the execution of current filter as its not the most effective filter implementing the policy {typeof(IAntiforgeryPolicy)}.");
            }
            else
            {
                if (!authorizationFilter.ShouldValidate(context))
                    return;
                try
                {
                    await authorizationFilter.Antiforgery.ValidateRequestAsync(context.HttpContext);
                }
                catch (AntiforgeryValidationException ex)
                {
                    authorizationFilter.Logger.LogInformation(ex.Message, (Exception) ex);
                    context.Result = new ApiValidationAntifrogeryFailed();
                }
            }
        }

        private bool ShouldValidate(AuthorizationFilterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            return true;
        }
    }
}