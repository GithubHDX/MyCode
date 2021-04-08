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
    public class GenericController : Controller
    {
        public ActionResult Index()
        {
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
    }

}
