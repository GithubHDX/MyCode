using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace delegatedemo.Controllers
{
    /// <summary>
    /// 泛型
    /// </summary>
    public class TController : Controller
    {
        public ActionResult Index()
        {
            main();
            return View();
        }

        public void main()
        {
            int[] arr = { 1, 8, 15, 6, 3 };
            forArrGenric(arr);
            Double[] douArr = { 10.5, 25.1, 4.9, 1.8 };
            forArrGenric(douArr);
            string[] strArr = { "我", "是", "字", "符", "串" };
            forArrGenric(strArr);
        }
        // 可以根据基类约束泛型的类型
        //public void forArrGenric<T>(T[] arr) where T : struct // 只允许是值类型
        //public void forArrGenric<T>(T[] arr) where T : class // 只允许是引用类型
        public void forArrGenric<T>(T[] arr) // 泛型方法 免去了装箱拆箱的操作 很好的起到了代码复用的效果
        {
            for (int i = 0; i < arr.Length; i++)
            {
                System.Diagnostics.Debug.WriteLine(arr[i]);
            }
        }
    }

}
