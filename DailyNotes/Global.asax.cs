using DailyNotes.Log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace DailyNotes
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["UserCount"] = 0;
            FlashLogger.Instance().Register();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                Application.Lock();
                int userCount = (int)Application["UserCount"];
                Application["UserCount"] = ++userCount;

                //注册路由
                //RouteTable.Routes.MapPageRoute(
                //    "product-search",
                //    "albums/search/{term}",
                //    "~/WebFormsRoute.aspx"
                //);
            }
            finally
            {
                Application.UnLock();
            }
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}