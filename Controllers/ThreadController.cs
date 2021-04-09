using delegatedemo.LogicCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace delegatedemo.Controllers
{
    /// <summary>
    /// 线程 - 线程池 - Task
    /// </summary>
    public class ThreadController : Controller
    {
        public ActionResult Index()
        {
            #region 1.ThreadStart创建线程 Thread启动线程 没有参数也没有返回值
            //for (int i = 0; i < 10; i++)
            //{
            //    ThreadStart threadStart = new ThreadStart(SayHello);
            //    Thread thread = new Thread(threadStart);
            //    thread.Start();
            //}
            #endregion
            #region 2.ParameterizedThreadStart创建线程 有1个参数但是没有返回值
            //for (int i = 0; i < 10; i++)
            //{
            //    ParameterizedThreadStart threadStart1 = new ParameterizedThreadStart(SayHello);
            //    Thread thread1 = new Thread(threadStart1);
            //    thread1.Start(i);
            //}
            #endregion
            #region 3.使用专门的线程类 多个参数,多个返回值 这个不中
            //MyThread mt = new MyThread(100);
            //ThreadStart threadStart = new ThreadStart(mt.SayGoodBye);
            //Thread thread = new Thread(threadStart);
            //thread.Start();
            #endregion

            #region 4.对比线程与线程池
            // 主程序等待子线程全部执行完毕才结束
            for (int i = 1; i <= 100; i++)
            {
                ParameterizedThreadStart threadStart = new ParameterizedThreadStart(SayHello);
                Thread thread = new Thread(threadStart);
                thread.Start(i);
            }
            System.Diagnostics.Debug.WriteLine("主线程执行！");
            System.Diagnostics.Debug.WriteLine("主线程结束！");
            System.Diagnostics.Debug.WriteLine("线程池终止！");
            //Console.ReadKey();

            // 主程序不会等待子线程
            //ThreadPool可以设置最大线程数
            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.SetMaxThreads(5, 5);
            for (int i = 1; i <= 1000; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(SayHello), i.ToString());
            }
            System.Diagnostics.Debug.WriteLine("主线程执行！");
            System.Diagnostics.Debug.WriteLine("主线程结束！");
            System.Diagnostics.Debug.WriteLine("线程池终止！");
            //Console.ReadKey();
            #endregion

            #region Task 异步 执行多个事务 多参数 各有返回值
            // Task 用factory创建会直接执行 用new创建的不会直接执行需要start才会执行
            #region Task多线程执行 list的方式获取返回值 异步执行最后获取结果 速度很快
            #region task list 耗时2.127081秒
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();//计算耗时
            sw.Start();
            List<Task<int>> tasklist = new List<Task<int>> { };
            for (int i = 0; i < 10; i++)
            {
                int index = i;
                var task = new Task<int>(() =>
                {
                    var indexres = sleep1000(index);
                    System.Diagnostics.Debug.WriteLine(index);
                    return indexres;
                });
                task.Start();
                tasklist.Add(task); // 获取线程执行完成后的返回值
            }
            var rescount = 0;
            foreach (var item in tasklist)
            {
                rescount += item.Result;
            }
            System.Diagnostics.Debug.WriteLine(rescount);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(sw.Elapsed.TotalSeconds + "秒");
            #endregion

            #region 对比一下同步方法 不用线程的话会很慢 耗时10.0841525秒
            System.Diagnostics.Stopwatch sw1 = new System.Diagnostics.Stopwatch();//计算耗时
            sw1.Start();
            int rescount1 = 0;
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(1000);
                rescount1 += i;
            }
            System.Diagnostics.Debug.WriteLine(rescount1);
            sw1.Stop();
            System.Diagnostics.Debug.WriteLine(sw1.Elapsed.TotalSeconds + "秒");
            #endregion

            #region 对比一下不用list获取结果的方法 这种就变成同步等待了 就和同步方法差不多了
            //（具体为啥不知道 可能是因为每次都要等到Result才能进行下一个线程吧） 耗时10.1307466秒
            System.Diagnostics.Stopwatch sw2 = new System.Diagnostics.Stopwatch();//计算耗时 
            sw2.Start();
            int rescount2 = 0;
            for (int i = 0; i < 10; i++)
            {
                int index = i;
                var task = new Task<int>(() =>
                {
                    var indexres = sleep1000(index);
                    System.Diagnostics.Debug.WriteLine(index);
                    return indexres;
                });
                task.Start();
                rescount2 += task.Result; // 获取线程执行完成后的返回值
            }
            System.Diagnostics.Debug.WriteLine(rescount2);
            sw2.Stop();
            System.Diagnostics.Debug.WriteLine(sw2.Elapsed.TotalSeconds + "秒");
            #endregion

            #region Task不等待返回值 0.0044036秒 且输出是在index打印完成之前 
            System.Diagnostics.Stopwatch sw3 = new System.Diagnostics.Stopwatch();//计算耗时 
            sw3.Start();
            for (int i = 0; i < 100; i++)
            {
                int index = i;
                var task = new Task(() =>
                {
                    System.Diagnostics.Debug.WriteLine(index);
                });
                task.Start();
            }
            sw3.Stop();
            System.Diagnostics.Debug.WriteLine(sw3.Elapsed.TotalSeconds + "秒");
            #endregion
            #endregion
            #endregion

            #region Task的四种启动方法 1.start 2.run 3.factory 4.RunSynchronously
            #region 1.实例化的方式Start启动
            // Task的构造函数中的参数是Action委托(注：不是Action<>多个重载)，所以直接使用 ()=>{   }的方式传参，简洁明了，然后调用Start方式启动。
            // 无入参无出参
            Task taskstart = new Task(() => { SayHello(); });
            taskstart.Start(); //启动任务，将其调度到当前任务调度程序执行

            #endregion

            #region 2.实例化方式RunSynchronously同步启动
            // Task实例化的方式，然后调用同步方法RunSynchronously，进行线程启动。(PS: 类似委托开启线程，BeginInvoke是异步，而Invoke是同步)
            Task taskrs = new Task(() => { SayHello(); });
            taskrs.RunSynchronously(); //在当前任务调度程序上同步运行任务

            #endregion

            #region 3.TaskFactory工厂启动
            // 使用TaskFactory工厂的StartNew方法启动，其中TaskFactory工厂可以直接实例化，或者 Task.Factory(推荐)。
            TaskFactory factory = new TaskFactory();
            Task taskfactory1 = factory.StartNew(() => { SayHello(); });
            //或者
            Task taskfactory2 = Task.Factory.StartNew(() => SayHello());
            #endregion

            #region 4.调用Task类下的静态方法Run，进行启动 可以看做是factory方法的简写吧
            // 使用该方式启动，更加简洁，不需要实例化，也不需要调用Start方法，Run方法直接通过Action委托的方式进行传参即可（即: () => { } ）。
            Task.Run(() => { SayHello(); });
            #endregion
            #endregion

            #region 常用的Task的等待方法 WaitAny WaitAll WhenAny+ContinueWith WhenAll+ContinueWith ContinueWhenAny ContinueWhenAll
            List<Task> waittasklist = new List<Task> { };
            Task waitask = new Task(() => { });

            // 1.WaitAny（执行的线程等待其中任何一个线程执行完毕即可执行）
            Task.WaitAny(waittasklist.ToArray()); // tasklist中任意一个线程执行完成就OK
            Task.WaitAny(waitask); // waitanytask执行完成就OK

            // 2.WaitAll（执行的线程等待其中任何一个线程执行完毕即可执行）
            Task.WaitAll(waittasklist.ToArray()); // tasklist中所有线程执行完成就OK

            // 3.WhenAny+ContinueWith 当其中一个线程执行完成后，新开启了一个线程执行，继续执行新业务，所以执行过程中，不卡主线程。
            Task.WhenAny(waittasklist.ToArray()).ContinueWith((m) => { SayHello(); });

            // 4.WhenAll+ContinueWith 当其中所有线程执行完成后，新开启了一个线程执行，继续执行新业务，所以执行过程中，不卡主线程
            Task.WhenAll(waittasklist.ToArray()).ContinueWith((m) => { SayHello(); });

            // 5.TaskFactory的线程等待
            // 5.1 ContinueWhenAny = WhenAny+ContinueWith
            Task.Factory.ContinueWhenAny(tasklist.ToArray(), (m) => { SayHello(); });
            // 5.2 ContinueWhenAll = WhenAll+ContinueWith
            Task.Factory.ContinueWhenAll(tasklist.ToArray(), (m) => { SayHello(); });
            #endregion

            return View();
        }
        public int sleep1000(int i)
        {
            Thread.Sleep(1000);
            return i;
        }

        public class MyThread
        {
            public int Parame { get; set; }

            public int Result { get; set; }
            public MyThread(int parame)
            {
                this.Parame = parame;
            }
            public int SayGoodBye(object name)
            {
                System.Diagnostics.Debug.WriteLine("say SayGoodBye");
                return 1;
            }
        }
        public void SayHello()
        {
            System.Diagnostics.Debug.WriteLine("say hello");
        }
        public static void SayHello(object age)
        {
            System.Diagnostics.Debug.WriteLine(age + "say hello");
        }
        public static int SayHello(string name, int age)
        {
            System.Diagnostics.Debug.WriteLine(name + age);
            return 1;
        }
    }

}
