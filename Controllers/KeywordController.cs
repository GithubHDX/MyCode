using delegatedemo.LogicCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
    /// 深拷贝 / 浅拷贝
    /// </summary>
    public class KeyWordController : Controller
    {

        public ActionResult Index()
        {
            //RefAndOut();
            //ConstAndReadonly();
            CopyFunction();
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

        #region 深拷贝 / 浅拷贝
        public void CopyFunction()
        {
            // 准备一个list
            List<CopyClassModel> list = new List<CopyClassModel> { };
            for (int i = 0; i < 10; i++)
            {
                list.Add(new CopyClassModel { Id = i, Name = i + "One" });
            }
            // 浅拷贝
            //var newlist = new CopyClassModel[10];
            //list.CopyTo(newlist);
            var newlist = list;

            // 深拷贝
            var deepcopylist = DeepCopy.DeepCopyByBin(list); // 序列化的方式深拷贝
            
            //把原对象改掉
            foreach (var item in list)
            {
                item.Name = "update";
            }

            //分别打印两个对象
            System.Diagnostics.Debug.WriteLine("打印list");
            foreach (var item in list)
            {
                System.Diagnostics.Debug.WriteLine(item.Name);
            }
            System.Diagnostics.Debug.WriteLine("打印newlist");
            foreach (var item in newlist)
            {
                System.Diagnostics.Debug.WriteLine(item.Name);
            }
            System.Diagnostics.Debug.WriteLine("打印deepcopylist");
            foreach (var item in deepcopylist)
            {
                System.Diagnostics.Debug.WriteLine(item.Name);
            }
        }
        public class DeepCopy
        {
            public static T DeepCopyByBin<T>(T obj)
            {
                object retval;
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    //序列化成流
                    bf.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    //反序列化成对象
                    retval = bf.Deserialize(ms);
                    ms.Close();
                }
                return (T)retval;
            }
        }
        #endregion
        [Serializable]
        private class CopyClassModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
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
