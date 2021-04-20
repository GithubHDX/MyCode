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
    /// 委托和事件
    /// </summary>

    public delegate void DLGOut(string name);//类外部
    //public event DLGOutClass eventModel2; // 事件不允许定义在类的外部
    public class DLGController : Controller
    {
        /// <summary>
        /// 委托和事件可以把方法当成参数传递 
        /// </summary>
        /// <param name="name"></param>
        private delegate void DLG(string name); // 类内部
        private event DLG eventModel; // 事件在方法外部定义 在方法内部执行 

        public ActionResult Index()
        {
            // 类内部定义的委托
            DLG dLG = SayHello;
            dLG += SayGoodBye;
            dLG("哈尼");

            // 事件
            eventModel += SayHello;
            eventModel += SayGoodBye;
            eventModel("大胖小子");

            // 类外部定义的委托
            DLGOut dLGOut = SayHello;
            dLGOut += SayGoodBye;
            dLGOut("小伙子");

            // 匿名委托 最常用的匿名委托是lambda表达式
            DLG dLGnm = delegate (string name)
            {
                SayHello("没有委托名" + name);
            };
            dLGnm += (string name) =>
            {
                SayHello("去掉委托关键字" + name);
            };
            dLGnm += name => SayHello("去掉参数类型和括号" + name);
            dLGnm("匿名委托");

            BDLGClass bDLGClass = new BDLGClass();
            bDLGClass.BMain();
            return View();
        }

        public void SayHello(string name)
        {
            System.Diagnostics.Debug.WriteLine(name + "相信未来");
        }
        public void SayGoodBye(string name)
        {
            System.Diagnostics.Debug.WriteLine(name + "永远真诚");
        }
    }

    /*
     * 事件是一种特殊的委托
     * 事件的底层实现其实还是一个委托
     * 在委托的基础上做了一层包装，并且提供了add和remove方法
     * 
     * 事件相比委托有很好的封装性：
     * 委托可以在类内和类外定义，委托可以在类内类外触发
     * 事件只允许在类的内部定义，事件只允许在类内触发
     */
    public class ADLGClass
    {
        public delegate void ADLG();
        public ADLG aDLG;
        public event ADLG AEvent;
        public void Function1()
        {
            System.Diagnostics.Debug.WriteLine("相信未来");
        }
        public void Function2()
        {
            System.Diagnostics.Debug.WriteLine("热爱生活");
        }
    }
    public class BDLGClass
    {
        public void BMain()
        {
            ADLGClass aDLGClass = new ADLGClass();
            aDLGClass.aDLG = aDLGClass.Function1;
            aDLGClass.aDLG += aDLGClass.Function2;
            aDLGClass.aDLG();

            //aDLGClass.AEvent += aDLGClass.Function1;
            //aDLGClass.AEvent += aDLGClass.Function2;
            //aDLGClass.AEvent();
        }
    }
}
