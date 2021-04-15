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
    /// const 和 static readonly的差别
    /// </summary>
    public class ConstReadonlyController : Controller
    {
        public const int constParam = 5 * 1; // 在编译期间确定 只能以常量的方式赋值
        public static readonly int staticreadonlyParam = 5;
        public static readonly int staticreadonlyParam1 = 5 * staticreadonlyParam1; // 在运行时确定 可以通过静态构造函数赋值
        public static readonly int staticreadonlyParam2 = GetParaNum();
  
        public static int GetParaNum()
        {
            return 1;
        }
        public ActionResult Index()
        {
            return View();
        }
    }

}
