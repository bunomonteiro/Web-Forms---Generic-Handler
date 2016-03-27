using System;
using System.Web;
using System.Web.Compilation;
using System.Web.Routing;

namespace HttpHandlers.Handlers
{
    public class HttpHandlerRoute : IRouteHandler
    {
        private readonly string _virtualPath;

        public HttpHandlerRoute(string virtualPath)
        {
            if (virtualPath == null) { throw new ArgumentNullException(nameof(virtualPath)); }
            this._virtualPath = virtualPath;
        }

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return (IHttpHandler)BuildManager.CreateInstanceFromVirtualPath(this._virtualPath, typeof(IHttpHandler));
        }
    }
}