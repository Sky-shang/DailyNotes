using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyNotes
{
    public partial class APM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PrintDataAsync();
            Debug.Print("three");



            WebRequest webRequest = WebRequest.Create("http://www.baidu.com");
            webRequest.BeginGetResponse(result =>
            {
                WebResponse webResponse = null;
                try
                {
                    webResponse = webRequest.EndGetResponse(result);
                    using (System.IO.FileStream fs = new System.IO.FileStream("$(ProjectDir)/baidu.html", System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    {
                        using (System.IO.Stream webStream = webResponse.GetResponseStream())
                        {
                            webStream?.CopyTo(fs);
                        }
                    }
                }
                catch (WebException ex)
                {
                    Response.Write("Failed:" + ex.GetBaseException().Message);
                }
                finally
                {
                    webResponse?.Close();
                }
            }, null);
        }
        private async void PrintDataAsync()
        {
            Task<int> result = CalculateDataAsync();
            Debug.Print("second");
            int data = await result;
            Debug.Print("last:" + data);
        }

        private async Task<int> CalculateDataAsync()
        {
            Debug.Print($"{Thread.CurrentThread.ManagedThreadId} : {Thread.CurrentThread.IsThreadPoolThread}");
            Debug.Print("first");
            int result = 0;
            for (int i = 0; i < 10; i++)
            {
                result += i;
            }
            await Task.Delay(1000);
            Debug.Print("four");
            Debug.Print($"{Thread.CurrentThread.ManagedThreadId} : {Thread.CurrentThread.IsThreadPoolThread}");
            return result;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            WebRequest webRequest = WebRequest.Create("http://www.baidu.com");
            TaskFactory factroy = new TaskFactory();
            Task<WebResponse> task = factroy.FromAsync<WebResponse>(webRequest.BeginGetResponse, webRequest.EndGetResponse, TaskCreationOptions.None);
            task.ContinueWith(t =>
            {
                WebResponse webResponse = null;
                try
                {
                    webResponse = t.Result;
                    using (System.IO.FileStream fs = new System.IO.FileStream("$(ProjectDir)", System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    {
                        using (System.IO.Stream webStream = webResponse.GetResponseStream())
                        {
                            webStream.CopyTo(fs);
                        }
                    }
                }
                catch (WebException ex)
                {
                    Response.Write("Failed:" + ex.GetBaseException().Message);
                }
                finally
                {
                    webResponse?.Close();
                }
            });
        }

        //假设：这是我们想要异步执行的方法，它有三个参数，和一个自定义类型的返回值
        private UserInfo OurFunction(string param1, string param2, string param3)
        {
            //...
            //...
            UserInfo uInfo = new UserInfo();
            return uInfo;
        }
        //第一步：定义一个和它签名一致的代理类型
        private delegate UserInfo OurFunctionDelegate(string param1, string param2, string param3);


        private void MainFunction()
        {
            //第二步：声明这个代理类型
            OurFunctionDelegate hander = new OurFunctionDelegate(OurFunction);
            //第三步：调用BgeinInvoke方法，同时传入一个回调函数
            hander.BeginInvoke("p1", "p2", "p3", AsyncCallBack, hander);
        }
        //第四步：定义一个回调函数
        private void AsyncCallBack(IAsyncResult result)
        {
            OurFunctionDelegate hander = (OurFunctionDelegate)result.AsyncState;

            try
            {
                //函数异步执行完成，会进入这个函数，调用EndInvoke方法可以得到结果，如有异常，也会在这里抛出
                UserInfo info = hander.EndInvoke(result);
            }
            catch (Exception)
            {
                //处理异步操作的异常，当然，这里的异常应该是ArrgregateException。
            }
        }

        public class UserInfo
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public async void TestAsync()
        {
            WebRequest webRequest = WebRequest.Create("http://www.baidu.com");
            await webRequest.GetResponseAsync();
        }
    }
}