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
    /// 继承和多态
    /// 继承是子类能用父类的公共和保护属性及方法
    /// 多态是父类能持有子类对象并访问子类重写的重名的方法（抽象函数或虚函数）
    /// </summary>
    public class ParentChildController : Controller
    {
        public ActionResult Index()
        {
            Main();
            return View();
        }
        public void Main()
        {
            Parent child = new Child();
            // 也可以通过抽象函数的方式实现父类调用子类的方法
            System.Diagnostics.Debug.WriteLine(child.ParentWithChild()); // 输出child
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

        public const int constParam = 5 * 1; // 只能以常量的方式赋值
        public static readonly int staticreadonlyParam = 5;
        public static readonly int staticreadonlyParam1 = 5 * staticreadonlyParam1;
        public static readonly int staticreadonlyParam2 = GetParaNum();

        public static int GetParaNum()
        {
            return 1;
        }
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
