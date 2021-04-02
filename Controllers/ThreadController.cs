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
    /// 线程
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
            #region Task 异步 执行多个事务 多参数 各有返回值
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
                tasklist.Add(task); // 物料获取线程执行完成后的返回值
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
                rescount2 += task.Result; // 物料获取线程执行完成后的返回值
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
