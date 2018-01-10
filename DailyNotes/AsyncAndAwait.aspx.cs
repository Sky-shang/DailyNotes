using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DailyNotes
{
    public partial class AsyncAndAwait : System.Web.UI.Page
    {
        private bool isDone = false;

        private object _lock = new object();

        private SemaphoreSlim _sem = new SemaphoreSlim(3);    // 我们限制能同时访问的线程数量是3

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write($"我是主线程:Thread ID {Thread.CurrentThread.ManagedThreadId}<br/>");
            //这里面需要注意的是，创建Thread的实例之后，需要手动调用它的Start方法将其启动。
            //但是对于Task来说，StartNew和Run的同时，既会创建新的线程，并且会立即启动它。
            new Thread(Go0).Start();
            Task.Factory.StartNew(Go0);
            Task.Run(new Action(Go0));
            //线程的创建是比较占用资源的一件事情，.NET 为我们提供了线程池来帮助我们创建和管理线程。
            //Task是默认会直接使用线程池，但是Thread不会。如果我们不使用Task，又想用线程池的话，可以使用ThreadPool类。
            ThreadPool.QueueUserWorkItem(Go);
            // 没有匿名委托之前，我们只能这样传入一个object的参数
            new Thread(Go).Start("arg1");
            new Thread(delegate ()
            {
                // 有了匿名委托之后...
                GoGoGo("arg1", "arg2", "arg3");
            }).Start();

            new Thread(() =>
            {
                // 当然,还有 Lambada
                GoGoGo("arg1", "arg2", "arg3");
            }).Start();

            Task.Run(() =>
            {
                // Task能这么灵活，也是因为有了Lambda呀。
                GoGoGo("arg1", "arg2", "arg3");
            });
            //Thread是不能返回值的，但是作为更高级的Task当然要弥补一下这个功能。
            var dayName = Task.Run<DayOfWeek>(() => DateTime.Now.DayOfWeek);
            Response.Write($"今天是：{dayName.Result}");
            //上面说了参数和返回值，我们来看一下线程之间共享数据的问题。
            new Thread(Done).Start();
            new Thread(Done).Start();
            //我们先把上面的代码小小的调整一下，就知道什么是线程安全了。我们把Done方法中的两句话对换了一下位置 。
            //出现双Done情况不会一直发生，但是如果你运气好的话，就会中奖了。因为第一个线程还没有来得及把_isDone设置成true，第二个线程就进来了，而这不是我们想要的结果，在多个线程下，结果不是我们的预期结果，这就是线程不安全。
            new Thread(DoneDone).Start();
            new Thread(DoneDone).Start();
            //要解决上面遇到的问题，我们就要用到锁。锁的类型有独占锁，互斥锁，以及读写锁等，我们这里就简单演示一下独占锁。
            //再我们加上锁之后，被锁住的代码在同一个时间内只允许一个线程访问，其它的线程会被阻塞，只有等到这个锁被释放之后其它的线程才能执行被锁住的代码。
            new Thread(Donelock).Start();
            new Thread(Donelock).Start();
            //Semaphore 信号量
            //我实在不知道这个单词应该怎么翻译，从官方的解释来看，我们可以这样理解。
            //它可以控制对某一段代码或者对某个资源访问的线程的数量，超过这个数量之后，其它的线程就得等待，只有等现在有线程释放了之后，
            //下面的线程才能访问。这个跟锁有相似的功能，只不过不是独占的，它允许一定数量的线程同时访问。
            for (int i = 1; i <= 5; i++) new Thread(Enter).Start(i);

            try
            {
                new Thread(Go1).Start();
            }
            catch (Exception ex)
            {
                // 其它线程里面的异常，我们这里面是捕获不到的。
                Response.Write("Exception!");
            }

            try
            {
                var task = Task.Run(() => { Go1(); });
                task.Wait();  // 在调用了这句话之后，主线程才能捕获task里面的异常

                // 对于有返回值的Task, 我们接收了它的返回值就不需要再调用Wait方法了
                // GetName 里面的异常我们也可以捕获到
                var task2 = Task.Run(() => { return GetName(); });
                var name = task2.Result;
            }
            catch (Exception ex)
            {
                Response.Write("Exception!");
            }
            Test(); // 这个方法其实是多余的, 本来可以直接写下面的方法
            // await GetName() 
            // 但是由于控制台的入口方法不支持async,所有我们在入口方法里面不能 用 await
        }
        public void Go0()
        {
            Response.Write($"我是Go0线程:Thread ID {Thread.CurrentThread.ManagedThreadId}<br/>");
        }
        static void Go1() { throw null; }
        public void Go(object data)
        {
            Response.Write($"我是Go线程:Thread ID {Thread.CurrentThread.ManagedThreadId}<br/>");
        }
        public void GoGoGo(string arg1, string arg2, string arg3)
        {
            Response.Write("多参数<br/>");
        }
        public void Done()
        {
            if (!isDone)
            {
                isDone = true;
                // 第二个线程来的时候，就不会再执行了(也不是绝对的，取决于计算机的CPU数量以及当时的运行情况)
                Response.Write("Done<br/>");
            }
        }
        public void DoneDone()
        {
            if (!isDone)
            {
                Response.Write("DoneDone<br/>");
                isDone = true;
            }
        }
        void Donelock()
        {
            lock (_lock)
            {
                if (!isDone)
                {
                    Response.Write("DoneDoneDone<br/>"); // 猜猜这里面会被执行几次？
                    isDone = true;
                }
            }
        }
        public void Enter(object id)
        {
            Response.Write(id + " 开始排队...<br/>");
            _sem.Wait();
            Response.Write(id + " 开始执行！<br/>");
            Thread.Sleep(1000 * (int)id);
            Response.Write(id + " 执行完毕，离开！<br/>");
            _sem.Release();
        }
        public string GetName() { throw null; }
        public async Task Test()
        {
            // 方法打上async关键字，就可以用await调用同样打上async的方法
            // await 后面的方法将在另外一个线程中执行
            await GetName(new object());
        }
        public async Task GetName(object obj)
        {
            // Delay 方法来自于.net 4.5
            await Task.Delay(1000);
            // 返回值前面加 async 之后，方法里面就可以用await了
            Response.Write($"Current Thread Id :{Thread.CurrentThread.ManagedThreadId}");
            Response.Write("In antoher thread.....");
        }
    }
}