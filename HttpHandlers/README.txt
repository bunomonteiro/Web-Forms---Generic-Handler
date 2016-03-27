IMPORTANT

1- Configure new routes in Global.asax
2- Configure new default documents (index.html, default.cshtml, etc...) in web.config
3- All handlers inherit from BaseHttpHandler or implement IBaseHttpHandler
4- Implement 'Route' static property on each custom HttpHandler and use in RegisterRoutes method in Global.asax