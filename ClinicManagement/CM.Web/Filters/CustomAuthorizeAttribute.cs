using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CM.Web.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {


        public override void OnAuthorization(AuthorizationContext actionContext)
        {

            if (actionContext.RequestContext.HttpContext.Session == null || actionContext.RequestContext.HttpContext.Session["SessionId"] == null)
            {
                if (actionContext.HttpContext.Request.IsAjaxRequest()) actionContext.HttpContext.Items["AjaxPermissionDenied"] = true;
                RouteValueDictionary rd = new RouteValueDictionary();
                rd.Add("controller", "Account");
                rd.Add("action", "Login");
                actionContext.Result = new RedirectToRouteResult("Admin", rd);
                actionContext.Result = new RedirectResult("~/Home/Index");
            }
            if (!string.IsNullOrWhiteSpace(this.Roles))
            {
                foreach (string role in this.Roles.Split(',').ToList())
                {
                    if (actionContext.HttpContext.User.IsInRole(role))
                    {
                        return;
                    }
                }

                actionContext.Controller.TempData["ErrorDetails"] = "You don't have access rights to this page";
                actionContext.Result = new RedirectResult("~/Home/Index");
                return;
            }
            base.OnAuthorization(actionContext);
        }


    }
}