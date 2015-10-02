using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingWeb.Filter
{
    public class AdminFilter : AuthorizeAttribute
    {
        /**
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"]))
            {
                filterContext.Result = new ContentResult()
                {
                   
                    Content = "Unauthorized to access specified resource."
                };
                
            }
        }**/

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!Convert.ToBoolean(httpContext.Session["IsAdmin"]))
            {
                return false;

            }

            return base.AuthorizeCore(httpContext);
        }

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    if (AuthorizeCore(filterContext.HttpContext))
        //    {
        //        base.OnAuthorization(filterContext);
        //    }
        //    else
        //    {
        //        filterContext.Controller.ViewData["AuthorizationError"] = "You do not have permission to take this action.";
        //    }
        //}
    }
}