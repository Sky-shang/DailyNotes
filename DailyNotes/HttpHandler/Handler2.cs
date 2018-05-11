using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyNotes.HttpHandler
{
    public class Handler2 : IHttpHandler
    {

        #region IHttpHandler 成员
        public bool IsReusable

        {

            get { return true; }

        }
        public void ProcessRequest(HttpContext context)
        {

            context.Response.Write("<html><body><h1>来自Handler2的信息。</h1></body></html>");

        }
        #endregion

    }

}