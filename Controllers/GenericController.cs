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
    /// 泛型和对象
    /// </summary>
    public class GenericController : Controller
    {
        public ActionResult Index()
        {
            // 泛型是以类型参数化的方式实现代码复用的一种方式 能够有效减少装箱拆箱的操作


            #region 泛型和object的差别
            int a = (int)getobjectvalue(1); // 需要装箱拆箱
            string b = getobjectvalue("B").ToString();
            // 即使类型错误编译器也不会报错 但是在运行时拆箱的时候就会错误跳出了
            int a1 = (int)getobjectvalue("B");
            //int a1 = int.Parse(getobjectvalue("B"));
            string b1 = getobjectvalue(1).ToString();
            System.Diagnostics.Debug.WriteLine(a);
            System.Diagnostics.Debug.WriteLine(b);

            int c = getvalue<int>(2); // 有类型的约束
            string d = getvalue<string>("D");
            // 类型不对时编译器会报错
            //int c1 = getvalue<int>("D");
            //string d1 = getvalue<string>(2);
            System.Diagnostics.Debug.WriteLine(c);
            System.Diagnostics.Debug.WriteLine(d);
            #endregion

            main();
            return View();



        }
        public object getobjectvalue(object a)
        {
            return a;
        }
        public T getvalue<T>(T a)
        {
            return a;
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
