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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string> { }; // 字典的声明
            List<Model> list = new List<Model> { };
            for (int i = 0; i < 10; i++)
            {
                keyValuePairs.Add(i, i.ToString()); // Add方法
                list.Add(new Model { Id = i, Name = i.ToString() });
            }

            return View();
        }
    }


    public class Model
    {
        public int Id { get; set; }
        public string Name{ get; set; }
    }

}



