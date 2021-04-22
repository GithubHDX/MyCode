using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace delegatedemo.Controllers
{
    public class RewriteController : Controller
    {
        /// <summary>
        /// 重写 抽象函数 虚函数
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region 虚函数
            #region 虚函数未在子类中重写
            BaseClassA mode = new ChildClassA();
            var sayhello = mode.SayHello("胡老板");
            // sayhello调用的是父类的方法
            // sayhello = "parent say hello胡老板"
            #endregion

            #region 虚函数在子类中重写
            // 1.父类声明子类实例化
            BaseClassA mode1 = new ChildClassA();
            var sayhello1 = mode1.SayHello("胡老板");
            // sayhello调用的是子类的方法
            // sayhello1 = "child say hello胡老板"

            // 2.父类声明父类实例化 
            BaseClassA mode2 = new BaseClassA();
            var sayhello2 = mode2.SayHello("胡老板");
            // sayhello调用的是父类的方法
            // sayhello2 = "parent say hello胡老板"

            // 3.子类声明子类实例化 
            ChildClassA mode3 = new ChildClassA();
            var sayhello3 = mode3.SayHello("胡老板");
            // sayhello调用的是子类的方法
            // sayhello3 = "child say hello胡老板"
            #endregion
            #endregion

            #region 抽象函数-属于纯虚函数 
            // 不存在基类声明基类实例化的情况调取SayGoodBye的方法，因为基类没办法访问这个方法
            BaseClassB model1 = new ChildClassB(); // 父类持有子类的对象 运行时若发现方法在子类中被重写就会访问子类的方法
            var saygoodbye1 = model1.SayGoodBye("胡老板");
            ChildClassB model2 = new ChildClassB();
            var saygoodbye2 = model2.SayGoodBye("胡老板");
            #endregion

            // 当声明为基类的时候 可以随意实例化为子类
            BaseClassTest testmodel;
            testmodel = new BaseClassTest();
            var test1 = testmodel.SayTest("胡老板"); // test1 say test胡老板
            testmodel = new ChildClassTest();
            var test2 = testmodel.SayTest("胡老板"); // test2 say test胡老板
            testmodel = new ChildChildClassTest();
            var test3 = testmodel.SayTest("胡老板"); // test3 say test胡老板
            testmodel = new ChildOtherClassTest();
            var test4 = testmodel.SayTest("胡老板"); // OtherTest say test胡老板
            return View();
        }
    }

    public class BaseClassA // 虚函数示例 基类
    {
        public virtual string SayHello(string name) // 虚函数在基类中必须声明函数主体，可以在子类中被重写覆盖/也可以不被重写
        {
            return "parent say hello" + name;
        }
    }
    public class ChildClassA : BaseClassA // 虚函数示例 派生类
    {
        public override string SayHello(string name) // 关键字override重写基类的方法
        {
            return "child say hello" + name;
        }
    }

    public abstract class BaseClassB // 抽象函数示例 基类 -- 抽象函数只能声明在抽象类中
    {
        public abstract string SayGoodBye(string name); // 抽象函数没有方法体
    }
    public class ChildClassB : BaseClassB // 抽象函数示例 派生类
    {
        public override string SayGoodBye(string name) // 派生类必须实现基类的抽象方法
        {
            return "child say hello" + name;
        }
    }

    public class BaseClassTest // 基类
    {
        public virtual string SayTest(string name) 
        {
            return "test1 say test" + name;
        }
    }
    public class ChildClassTest : BaseClassTest  // 派生类第一层
    {
        public override string SayTest(string name) 
        {
            return "test2 say test" + name;
        }
    }
    public class ChildChildClassTest : ChildClassTest // 派生类第二层
    {
        public override string SayTest(string name) 
        {
            return "test3 say test" + name;
        }
    }
    public class ChildOtherClassTest : BaseClassTest  // 另外一个派生类
    {
        public override string SayTest(string name)
        {
            return "OtherTest say test" + name;
        }
    }
}
