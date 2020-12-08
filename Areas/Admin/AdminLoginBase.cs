using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BurgerQ.Areas.Admin
{
    public class AdminLoginBase : Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var islogin = false;
            if (requestContext.HttpContext.Session["AdminLoginUser"]==null)
            {
                //Admin girmeyib
                requestContext.HttpContext.Response.Redirect("/Admin/AdminLogin");
            }
            else
            {

                //admin girib
            }
            base.Initialize(requestContext);
        }
    }
}