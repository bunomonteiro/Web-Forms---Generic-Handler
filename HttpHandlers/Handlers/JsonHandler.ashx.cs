using System.Text;
using System.Web;
using System.Web.Routing;

namespace HttpHandlers.Handlers
{
    /// <summary>
    ///     Summary description for JsonHandler
    /// </summary>
    public class JsonHandler : BaseHttpHandler
    {
        public static Route Route { get; } = new Route("Handlers/Json/{*action}", new HttpHandlerRoute("~/Handlers/JsonHandler.ashx"));
        public override void DefaultAction(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.ContentEncoding = Encoding.UTF8;

            var json = this.CreateJsonString();

            //context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-60));
            //context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Write(json);
        }

        private string CreateJsonString()
        {
            var json = new StringBuilder();
            json.Append("{");
            json.Append("\n	name: 'Brazil',");
            json.Append("\n	capital: 'Brasília',");
            json.Append("\n	largestCity: 'São Paulo',");
            json.Append("\n	OfficialLanguages: ['Portuguese'],");
            json.Append("\n	ethnicGroups: [");
            json.Append("\n		{ name: 'White', percentage: 47.73 },");
            json.Append("\n		{ name: 'Multiracial', percentage: 43.13 },");
            json.Append("\n		{ name: 'Black', percentage: 7.61 },");
            json.Append("\n		{ name: 'Asian', percentage: 1.09 },");
            json.Append("\n		{ name: 'Amerindian', percentage: 0.43 }");
            json.Append("\n	],");
            json.Append("\n	demonym: 'Brazilian',");
            json.Append("\n	government: 'Federal presidential constitutional republic',");
            json.Append("\n	area: {total: 8515767, unit: 'km2' },");
            json.Append("\n	waterPercentage: 0.65,");
            json.Append("\n	population: {");
            json.Append("\n		estimate: [{year: 2015, total: 205338000}],");
            json.Append("\n		density: {total: 23.8, unit: 'km2' }");
            json.Append("\n	},");
            json.Append("\n	currency: {");
            json.Append("\n		name: 'Real',");
            json.Append("\n		code: 'BRL',");
            json.Append("\n		symbol: 'R$'");
            json.Append("\n	},");
            json.Append("\n	dateFormat: 'dd/MM/yyyy'");
            json.Append("\n}");

            return json.ToString();
        }
    }
}