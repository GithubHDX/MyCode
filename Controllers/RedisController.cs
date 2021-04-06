using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace delegatedemo.Controllers
{
    public class RedisController : Controller
    {
        // GET: Redis
        public ActionResult Index()
        {
            return View();
        }
    }
}