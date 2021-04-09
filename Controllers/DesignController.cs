using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace delegatedemo.Controllers
{
    /// <summary>
    /// 设计模式
    /// </summary>
    public class DesignController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
    #region 单例模式
    /// <summary>
    /// 单例模式 （双重判断）
    /// </summary>
    public class Singleton
    {
        private static Singleton instance;
        private static object Singleton_Lock = new object();
        private Singleton() { }
        public static Singleton getInstance()
        {
            if (instance == null) // 第一次判断
            {
                lock (Singleton_Lock) // 加锁
                {
                    if (instance == null) // 第二次判断
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
    #region 1.1懒汉模式 判空创建实例 多线程不安全 因为没有加锁
    public class SingletonLazy
    {
        private static SingletonLazy instance;
        private SingletonLazy() { }

        public static SingletonLazy getinstance()
        {
            if (instance == null)
            {
                instance = new SingletonLazy();
            }
            return instance;
        }
    }
    #endregion
    #region 1.2懒汉模式 判空加锁 多线程安全
    public class SingletonLazyLock
    {
        private static object lazylock = new object();

        private static SingletonLazyLock instance;
        private SingletonLazyLock() { }

        public static SingletonLazyLock getinstance()
        {
            if (instance == null)
            {
                lock (lazylock)
                {
                    instance = new SingletonLazyLock();
                }
            }
            return instance;
        }
    }
    #endregion
    #region 2.饿汉模式 只要访问就给一个实例 有资源浪费（创造太多实例了）
    public class singletonhungry
    {
        private static singletonhungry instance;
        private singletonhungry() { }

        public static singletonhungry getinstance()
        {
            instance = new singletonhungry();
            return instance;
        }
    }
    #endregion
    #endregion

}
