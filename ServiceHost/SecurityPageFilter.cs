using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace ServiceHost
{
    public class SecurityPageFilter : IPageFilter
    {
        private readonly IAuthHelper _authHelper;

        public SecurityPageFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {   
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var handlerPermission = (NeedsPermissionAttribute)context.HandlerMethod.MethodInfo.GetCustomAttribute(typeof(NeedsPermissionAttribute));
            if (handlerPermission == null)
                return;
            
            var currentUserPermissions = _authHelper.GetCurrentUserPermissions();

            if (!currentUserPermissions.Contains(handlerPermission.Permission))
                context.HttpContext.Response.Redirect("/AccessDenied");

        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}
