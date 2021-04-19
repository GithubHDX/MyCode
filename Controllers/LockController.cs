using delegatedemo.LogicCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
//using System.Web.Http;
using System.Web.Mvc;

namespace delegatedemo.Controllers
{
    /// <summary>
    /// 加锁时静态锁和非静态锁的差别
    /// </summary>
    public class LockController : Controller// ApiController
    {
        private static readonly object staticLockObject;
        private readonly object lockObject;
        public void GetParaNum()
        {
            lock (staticLockObject)
            // 对于静态锁 相当于是一个房间只有个门，每个门只有一把钥匙，一次只允许一个人进入 
            // 假如房内有三张床 则相当于一个人占了三张床 就比较浪费 但是可以保证这个房间只有一个人
            {
                // ...
            }
            lock (lockObject)
            // 对于非静态锁 相当于是一个房间有多个门，每个门有一把钥匙，就可能存在多个人同时进入房间
            // 可以出现三张床各有人用 不会浪费 但是有可能出现两个人躺同一个床 锁就相当于无效了
            {
                // ...
            }
        }

        //[HttpPost, HttpGet] // 这个暂时搞不定不搞了
        //[CheckToken]
        //public HttpResponseMessage TestApi([FromBody] string args)
        //{
        //    return new HttpResponseMessage { };
        //}

    }

}
