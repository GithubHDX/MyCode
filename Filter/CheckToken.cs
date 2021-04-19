using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace delegatedemo.Filter
{
    public class CheckTokenAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 权限点
        /// </summary>
        public int Power { get; set; }
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="power">权限点(目前未实现)</param>
        /// <param name="error">错误提示信息(目前未实现)</param>
        public CheckTokenAttribute(int power = 0, string error = "")
        {
            Power = power;
            Error = error;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            return;
        }
    }
}