using System.Web;
using System.Web.Routing;

namespace HttpHandlers.Handlers
{
    /// <summary>
    ///     Summary description for IndexHandler
    /// </summary>
    public class IndexHandler : BaseHttpHandler
    {
        /// <summary>
        ///     url: DOAMIN/Handlers/ImageHandler.ashx
        ///     or url: DOAMIN/Handlers/ImageHandler.ashx?action=default
        /// </summary>
        public override void DefaultAction(HttpContext context)
        {
            context.Response.ContentType = "image/jpg";
            context.Response.WriteFile("~/Images/image1.jpg");
        }
    }
}