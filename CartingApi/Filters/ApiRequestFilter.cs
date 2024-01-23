using CorrelationId.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CartingApi.Filters;

public sealed class ApiRequestFilter : ActionFilterAttribute
{
    public ApiRequestFilter(IApiRequestTracker apiRequestTracker, ICorrelationContextAccessor correlationContextAccessor)
    {
        _correlationContextAccessor = correlationContextAccessor ?? throw new ArgumentNullException(nameof(correlationContextAccessor));
    }

    private readonly ICorrelationContextAccessor _correlationContextAccessor;

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!Guid.TryParse(_correlationContextAccessor.CorrelationContext.CorrelationId, out Guid correlationId))
        {
            context.Result = new BadRequestResult();
            return;
        }

        await next.Invoke();
    }

    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        await next.Invoke();
    }
}
