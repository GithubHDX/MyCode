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
    public class DictionaryController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<int, string> keyValuePairs = new Dictionary<int, string> { }; // 字典的声明
            for (int i = 0; i < 10; i++)
            {
                // 需要注意，如果键值已经存在，重复添加键值，运行时报错
                // 因此最好在每次add前做key值检查
                if (!keyValuePairs.ContainsKey(i))
                {
                    // Add方法 
                    keyValuePairs.Add(i, i.ToString()); 
                    //keyValuePairs[i] = i.ToString(); 也可以直接添加
                }
            }
            var a = keyValuePairs[1]; // 根据key获取value的方法
            System.Diagnostics.Debug.WriteLine(a);
            keyValuePairs.Remove(1); // 根据key删除键值对

            //var b =  keyValuePairs[1]; // 查询不存在的key,运行时报错,可以在查询前检查key是否存在
            return View();
        }
    }
}



