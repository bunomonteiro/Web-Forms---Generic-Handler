using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace HttpHandlers.Handlers
{
    public interface IBaseHttpHandler : IHttpHandler, IRequiresSessionState
    {
        int ActionsLength { get; }
        List<string> AllActionNames { get; }
        List<KeyValuePair<string, Action<HttpContext>>> Actions { get; }
        string ActionParameterName { get; }

        void DefaultAction(HttpContext context);
        void AddAction(string name, Action<HttpContext> action);
        void RemoveAction(string name);
        Action<HttpContext> GetAction(string name);

        void OnException(HttpContext context, Exception exception);
    }
}