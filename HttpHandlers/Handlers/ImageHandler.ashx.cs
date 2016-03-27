using System.Web;
using System.Web.Routing;

namespace HttpHandlers.Handlers
{
    /// <summary>
    ///     Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : BaseHttpHandler
    {
        public ImageHandler() { this.AddAction("second", this.SecondAction); }

        public static Route Route { get; } = new Route("Handlers/Image/{*action}", new HttpHandlerRoute("~/Handlers/ImageHandler.ashx"));

        /// <summary>
        ///     url: DOMAIN/Handlers/ImageHandler.ashx
        ///     or url: DOMAIN/Handlers/ImageHandler.ashx?action=default
        /// </summary>
        public override void DefaultAction(HttpContext context)
        {
            context.Response.ContentType = "image/jpg";
            context.Response.WriteFile("~/Images/image1.jpg");
        }

        /// <summary>
        ///     url: DOMAIN/Handlers/ImageHandler.ashx?action=second
        ///     The action "second" was mapped on constructor using AddAction method
        /// </summary>
        public void SecondAction(HttpContext context)
        {
            context.Response.ContentType = "image/jpg";
            context.Response.WriteFile("~/Images/image2.jpg");
        }
    }
}