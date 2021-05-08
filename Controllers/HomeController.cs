﻿using delegatedemo.LogicCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection; // 使用反射
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace delegatedemo.Controllers
{
    /// <summary>
    /// 反射
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}



