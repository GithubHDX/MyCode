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
    /// 泛型
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();

        }
    }

}
