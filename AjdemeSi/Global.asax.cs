using AjdemeSi.App_Start;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AjdemeSi.Business.Helpers;
using AutoMapper;
using System;

namespace AjdemeSi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.Configure();
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //log the error!
            ExceptionLogingHelper.LogException(ex);

        }
}
}
