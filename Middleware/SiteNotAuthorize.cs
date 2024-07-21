﻿using HHD.BL.Auth;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HHD.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SiteNotAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public SiteNotAuthorizeAttribute()
        {
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            ICurrentUser? currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
            if (currentUser == null)
            {
                throw new Exception("No user middleware");
            }

            bool isLoggedIn = await currentUser.IsLoggedIn();
            if (isLoggedIn == true)
            {
                context.Result = new RedirectResult("/");
                return;
            }
        }
    }
}