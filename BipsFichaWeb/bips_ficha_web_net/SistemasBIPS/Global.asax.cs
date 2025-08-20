using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SistemasBIPS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            HttpException httpException = ex as HttpException;
            String accion = string.Empty;
            if (httpException.GetHttpCode() == 404)
                accion = "Error404";
            else
                accion = "ErrorGeneral";
            Context.ClearError();
            RouteData rutaError = new RouteData();
            rutaError.Values.Add("controller","Error");
            rutaError.Values.Add("action", accion);
            IController controlador = new Controllers.ErrorController();
            controlador.Execute(new RequestContext(new HttpContextWrapper(Context), rutaError));
        }
    }
}
