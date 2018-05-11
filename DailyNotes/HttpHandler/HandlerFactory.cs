using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DailyNotes.HttpHandler
{
    public class HandlerFactory : IHttpHandlerFactory
    {
        #region IHttpHandlerFactory 成员

        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {

            string fname = url.Substring(url.IndexOf('/') + 1);

            while (fname.IndexOf('/') != -1)

                fname = fname.Substring(fname.IndexOf('/') + 1);

            string cname = fname.Substring(0, fname.IndexOf('.'));

            string className = "DailyNotes.HttpHandler." + cname;

            object h = null;
            try

            {
                // 采用动态反射机制创建相应的IHttpHandler实现类。
                h = Activator.CreateInstance(Type.GetType(className));
            }

            catch (Exception e)

            {

                throw new HttpException("工厂不能为类型" + cname + "创建实例。", e);

            }
            return (IHttpHandler)h;

        }
        public void ReleaseHandler(IHttpHandler handler)
        {



        }
        #endregion
    }
}