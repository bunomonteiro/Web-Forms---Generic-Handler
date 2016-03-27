using System;
using System.Web;
using System.Web.Routing;
using HttpHandlers.Handlers;

namespace HttpHandlers
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e) { this.RegisterRoutes(RouteTable.Routes); }

        private void RegisterRoutes(RouteCollection routes)
        {
            /* Asterisk (*) before parameter name is to set it as optional  */
            routes.Add("ImageHandler", ImageHandler.Route);
            routes.Add("JsonHandler", JsonHandler.Route);
        }
    }
}