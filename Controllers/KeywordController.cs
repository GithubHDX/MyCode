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
    /// 一些关键字的示例
    /// ref / out
    /// const / readonly
    /// </summary>
    public class KeyWordController : Controller
    {

        public ActionResult Index()
        {
            //RefAndOut();
            ConstAndReadonly();
            return View();
        }

        #region ref / out
        public void RefAndOut()
        {
            string refname = "oldrefname";
            RefFunction(ref refname);
            System.Diagnostics.Debug.WriteLine("传出的refname" + refname);

            string outname = "oldoutname";
            OutFunction(out outname);
            System.Diagnostics.Debug.WriteLine("传出的outname" + outname);
        }
        public void RefFunction(ref string refname)
        {
            System.Diagnostics.Debug.WriteLine("传入的refname:" + refname);
            refname = "updateref";
        }
        public void OutFunction(out string outname)
        {
            //System.Diagnostics.Debug.WriteLine("传入的outname:" + outname); 
            // out参数必须先赋值才能使用 否则会提示使用了未赋值的out参数
            outname = "updateout";
        }
        #endregion

        #region const / readonly
        // const 声明静态常量 声明之后就必须初始化
        // readionly 声明动态常量 允许以参数形式初始化
        const int constparm = 0;
        readonly int srparm = 0;

        static int a = 1;
        static int b = 2;
        //const int constparm1 =  a; a * b; 都不允许 必须是确定的值
        const int C1 = 5;
        const int C2 = C1 + C1;
        readonly int srparm1 = a * b; // 允许是动态常量初始化
      
        public void ConstAndReadonly()
        {
            const int constparm1 = 0; // const 允许在方法内声明常量
        }
        
        #endregion

    }
    /// <summary>
    /// readonly 最多允许延迟到构造函数来初始化 初始化完成之后就不允许修改了
    /// 在实例化的时候 通过Age(xxx)来确定实际值
    /// </summary>
    class Age
    {
        readonly int year;
        Age(int year) 
        {
            this.year = year;
        }
        void ChangeYear()
        {
            //year = 1967; // 初始化完成之后就不允许修改了
        }
    }
}
