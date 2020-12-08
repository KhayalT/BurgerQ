using BurgerQ.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BurgerQ.Controllers.Base
{
    public class LoginControllerBase : Controller
    {
        public bool IsLogin { get; private set; }
        public int LoginUserID { get;private set; }
        public User LoginUserEntity { get;private set; }
        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Session["LoginUserID"]!=null)
            {
                IsLogin = true;
                LoginUserID = (int)requestContext.HttpContext.Session["LoginUserID"];
                LoginUserEntity = (BurgerQ.Models.Entity.User)requestContext.HttpContext.Session["LoginUserEntity"];
                //giris edib
            }
            base.Initialize(requestContext);
        }
    }
}