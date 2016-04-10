using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace HttpHandlers.Handlers
{
    public abstract class BaseHttpHandler : IBaseHttpHandler
    {
        private const string DefaultActionParameterName = "action";
        private const string DefaultActionName = "default";
        private readonly Dictionary<string, Action<HttpContext>> _actions;

        private CustomErrorsSection _customErrorsSection;
        private string _actionParameterName;

        protected BaseHttpHandler()
        {
            this.ActionParameterName = DefaultActionParameterName;
            this._actions = new Dictionary<string, Action<HttpContext>>
            {
                {DefaultActionName, this.DefaultAction}
            };
        }

        public string ActionParameterName
        {
            get
            {
                return string.IsNullOrWhiteSpace(this._actionParameterName)
                           ? (this._actionParameterName = DefaultActionParameterName)
                           : this._actionParameterName;
            }
            protected set { this._actionParameterName = GetCleanName(value); }
        }

        public bool IsReusable => false;
        public int ActionsLength => this._actions.Count;
        public List<string> AllActionNames => this._actions.Keys.ToList();
        public List<KeyValuePair<string, Action<HttpContext>>> Actions => this._actions.ToList();

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var actionName = this.GetParameterValue(context, this.ActionParameterName);

                if (string.IsNullOrWhiteSpace(actionName))
                {
                    this._actions[DefaultActionName](context);
                }
                else if (this._actions.ContainsKey(actionName))
                {
                    this._actions[actionName](context);
                }
                else
                {
                    context.Response.ClearHeaders();
                    context.Response.ClearContent();

                    context.Response.TrySkipIisCustomErrors = true;
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.ContentType = "text/html";
                    context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write("<h1 style='color:#666666;font-size:75px;text-align:center'>404</h1>");
                    context.Response.Write("<h3 style='color:#666666;text-align:center'>Handler Not Found</h3>");
                    context.Response.Write($"<p style='color:#999999;text-align:center'>The requested URL <i>\"{context.Request.RawUrl}\"</i> was not found on this server.</p>");
                }
            }
            catch (Exception exception)
            {
                this.OnException(context, exception);
            }
        }

        public void AddAction(string name, Action<HttpContext> action)
        {
            if (string.IsNullOrWhiteSpace(name) || action == null) { return; }

            var cleanName = GetCleanName(name);
            if (!this._actions.ContainsKey(cleanName))
            {
                this._actions.Add(cleanName, action);
            }
        }

        public void RemoveAction(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return; }

            var cleanName = GetCleanName(name);
            if (this._actions.ContainsKey(cleanName))
            {
                this._actions.Remove(cleanName);
            }
        }

        public Action<HttpContext> GetAction(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return null; }

            Action<HttpContext> action = null;
            var cleanName = GetCleanName(name);
            if (this._actions.ContainsKey(cleanName))
            {
                action = this._actions[cleanName];
            }

            return action;
        }

        public void OnException(HttpContext context, Exception exception)
        {
            try
            {
                context.Response.ClearHeaders();
                context.Response.ClearContent();

                context.Response.TrySkipIisCustomErrors = true;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/html";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write("<h1 style='color:#666666;font-size:75px;text-align:center'>500</h1>");
                context.Response.Write("<h3 style='color:#666666;text-align:center'>Internal Server Error</h3>");
                context.Response.Write("<p style='color:#999999;text-align:center'>The server encountered an internal error and could not complete your request. the website administrator has been notified.</p>");

                if (this._customErrorsSection == null)
                {
                    this._customErrorsSection = (CustomErrorsSection)WebConfigurationManager.OpenWebConfiguration("/").GetSection("system.web/customErrors");
                }

                if (this._customErrorsSection != null && this._customErrorsSection.Mode == CustomErrorsMode.On)
                {
                    context.Response.Write($"<pre style='background-color:#EEEEEE;border:1px solid #999999;color:#666666;padding:15px;text-align:left'>{exception.Message}<br>{exception.StackTrace}</pre>");
                }
            }
            catch { /* ignored */ }
        }

        public abstract void DefaultAction(HttpContext context);

        protected string GetParameterValueFromRawUrl(HttpContext context, string parameterName)
            => GetCleanName(context.Request[parameterName]);

        protected string GetParameterValueFromRouteData(HttpContext context, string parameterName)
            => GetCleanName(Convert.ToString(context.Request.RequestContext.RouteData.Values[parameterName]));

        protected string GetParameterValue(HttpContext context, string parameterName)
            => this.GetParameterValueFromRawUrl(context, parameterName) ?? this.GetParameterValueFromRouteData(context, parameterName);

        private static string GetCleanName(string actionName) => !string.IsNullOrWhiteSpace(actionName)
                                                                    ? actionName.Replace(" ", "").ToLower()
                                                                    : null;
    }
}