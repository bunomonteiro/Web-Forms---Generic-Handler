using System;
using System.Web.UI;

namespace HttpHandlers
{
    public partial class Index : Page
    {
        protected void Page_Load(object sender, EventArgs e) => this.Session["TestKey"] = "Hello, world!";
    }
}