﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyNotes.HttpHandler
{
    public class JpgHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // 获取文件服务器端物理路径
            string fileName = context.Server.MapPath(context.Request.FilePath);
            // 如果UrlReferrer为空，则显示一张默认的禁止盗链的图片
            if (context.Request.UrlReferrer?.Host == null)
            {
                context.Response.ContentType = "image/JPEG";
                context.Response.WriteFile("/error.jpg");
            }
            else
            {
                // 如果 UrlReferrer中不包含自己站点主机域名，则显示一张默认的禁止盗链的图片
                if (context.Request.UrlReferrer.Host.IndexOf("host", StringComparison.Ordinal) > 0)
                {
                    context.Response.ContentType = "image/JPEG";
                    context.Response.WriteFile(fileName);
                }
                else
                {
                    context.Response.ContentType = "image/JPEG";
                    context.Response.WriteFile("/error.jpg");
                }
            }
        }

        public bool IsReusable => true;
    }
}