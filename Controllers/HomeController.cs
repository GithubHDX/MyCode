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
            Main();
            return View();
        }
        public void Main()
        {
            Parent child= new Child();
            System.Diagnostics.Debug.WriteLine(child.ParentWithChild()); // 输出child
            // 也可以通过抽象函数的方式实现父类调用子类的方法
        }
    }

    /// <summary>
    /// 父类
    /// </summary>
    public class Parent
    {
        private string ParentName;
        protected string ParentSchoolName;
        public string ParentCountryName;
        private void ParentFunPrivate() { }
        protected void ParentFunProtected() { }
        public void ParentFunPublic() { }

        public virtual string ParentWithChild() { return "parent"; }
    }
    /// <summary>
    /// 子类
    /// </summary>
    public class Child : Parent
    {
        private string ChildName;
        protected string ChildSchoolName;
        public string ChildCountryName;
        private void ChildFunPrivate() { }
        protected void ChildFunProtected() { }
        public void ChildFunPublic() { }
        public override string ParentWithChild() { return "child"; }
    }


}
