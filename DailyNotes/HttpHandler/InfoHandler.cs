using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DailyNotes.HttpHandler
{
    public class InfoHandler:IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string responseString = @"<!DOCTYPE HTML> 
                                        <html>
                                        <head>
                                            <meta charset='UTF-8'>
                                            <title>Sample Modules</title>
                                        </head>
                                        <body>
                                            <h1>Modules Loaded：</h1>
                                            <div>{0}</div>
                                        </body>
                                        </html>";
            var sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (var module in context.ApplicationInstance.Modules)
            {
                sb.AppendFormat("<li>{0}</li>",module);
            }
            sb.Append("</ul>");
            context.Response.ContentType = "text/html";
            context.Response.Write(string.Format(responseString,sb));
        }

        public bool IsReusable => true;
    }
}