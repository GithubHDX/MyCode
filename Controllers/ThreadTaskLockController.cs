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
    /// 多线程遇上委托 静态锁 静态变量
    /// </summary>
    public class ThreadTaskLockController : Controller
    {
        public delegate string TestDelegateNoResult();

        public ActionResult Index()
        {
            TestDelegateNoResult testDelegateNoResult = new TestDelegateNoResult(new TestLock().Addition);
            //这里实现的是调用部分 尝试在循环内以：
            // 1. thread 委托调用 2.thread 实例化调用 3.task 委托调用 4.task 实例化调用
            // 4种情况分别调用Addition方法

            for (int i = 0; i < 10; i++)
            {
                new Thread(() =>
                {
                    System.Diagnostics.Debug.WriteLine(testDelegateNoResult());
                    //System.Diagnostics.Debug.WriteLine(new TestLock().Addition());
                }).Start();
                //Task.Run(() =>
                //{
                //    System.Diagnostics.Debug.WriteLine(testDelegateNoResult());
                //    System.Diagnostics.Debug.WriteLine(new TestLock().Addition());
                //});
            }
            return View();
        }
    }


    public class TestLock
    {
        private object obj = new object();
        private static object static_object = new object();
        private static int staticTotal = 0;
        private int total = 0;
        // 这是一个尝试用非静态锁 obj，静态锁 static_object
        // 静态变量staticTotal，非静态变量total 查看累加后数值及使用线程情况的方法
        public string Addition()
        {
            lock (static_object) // obj
            {
                for (int i = 0; i <= 50; i++)
                {
                    staticTotal += i; // total
                    Thread.Sleep(5);
                }
                return staticTotal.ToString() + " thread:" + Thread.CurrentThread.ManagedThreadId;
            }
        }
    }

}



