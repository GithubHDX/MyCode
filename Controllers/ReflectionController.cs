using delegatedemo.LogicCode;
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
    /// 反射 特性
    /// </summary>
    public class ReflectionController : Controller
    {
        public ActionResult Index()
        {
            #region 获取一个类型所支持的方法
            //Type t = typeof(MyClass);//获得一个表示MyClass类的Type对象
            //System.Diagnostics.Debug.WriteLine("获取当前成员的名称" + t.Name);
            //System.Diagnostics.Debug.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            //System.Diagnostics.Debug.WriteLine("支持的方法");
            //#region 第一种形式
            ////MethodInfo[] mi = t.GetMethods();//显示Class类中被支持的方法
            //// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            ////方法GetMethods() 把 MyClass 的基类 object方法都显示出来了
            ////下面我们说说  GetMethods() 的另外一种形式，有限制的显示
            //#endregion
            //#region 第二种形式
            //MethodInfo[] mi = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            //#endregion
            //foreach (MethodInfo m in mi)
            //{
            //    //返回后打印出MyClass类中成员的类型(方法的返回值类型)极其方法名称
            //    System.Diagnostics.Debug.Write("  " + m.ReturnType.Name + "  " + m.Name + " (");//ReturnType获取此方法的返回类型
            //    ParameterInfo[] pi = m.GetParameters();//获得方法的参数
            //    for (int i = 0; i < pi.Length; i++)
            //    {
            //        System.Diagnostics.Debug.Write(pi[i].ParameterType.Name + "   " + pi[i].Name);//ParameterType 获取该参数的Type（类型）
            //        if (i + 1 < pi.Length)
            //        {
            //            System.Diagnostics.Debug.Write(", ");
            //        }

            //    }
            //    System.Diagnostics.Debug.WriteLine(")");
            //}
            #endregion
            #region 使用反射调用方法 用Invoke来获取返回值
            Type t = typeof(MyClass);
            MyClass reflectOb = new MyClass(10, 20);
            int val;
            System.Diagnostics.Debug.WriteLine("Invoke methods in " + t.Name);//调用MyClass类的方法
            MethodInfo[] mi = t.GetMethods();
            foreach (MethodInfo m in mi)//根据方法名和方法参数类型确定调用的具体方法，invoke能够执行具体的方法，并获取返回值
            {
                //获得方法参数
                ParameterInfo[] pi = m.GetParameters();
                if (m.Name.Equals("Set", StringComparison.Ordinal) && pi[0].ParameterType == typeof(int) && pi[1].ParameterType == typeof(string))
                {
                    object[] obj = new object[2];
                    obj[0] = 111;
                    obj[1] = "int+string";
                    m.Invoke(reflectOb, obj);
                }
                else if (m.Name.Equals("Set", StringComparison.Ordinal) && pi[0].ParameterType == typeof(int))
                {
                    //     指定 System.String.Compare(System.String,System.String) 和 System.String.Equals(System.Object)
                    //     方法的某些重载要使用的区域、大小写和排序规则。
                    //StringComparison.Ordinal   使用序号排序规则比较字符串 
                    object[] obj = new object[2];
                    obj[0] = 9;
                    obj[1] = 18;
                    m.Invoke(reflectOb, obj);
                }
                else if (m.Name.Equals("Set", StringComparison.Ordinal) && pi[0].ParameterType == typeof(double))
                {
                    object[] obj = new object[2];
                    obj[0] = 1.12;
                    obj[1] = 23.4;
                    m.Invoke(reflectOb, obj);
                }
                else if (m.Name.Equals("Sum", StringComparison.Ordinal))
                {
                    val = (int)m.Invoke(reflectOb, null);
                    System.Diagnostics.Debug.WriteLine("Sum is : " + val);
                }
                else if (m.Name.Equals("IsBetween", StringComparison.Ordinal))
                {
                    object[] obj = new object[1];
                    obj[0] = 14;
                    if ((bool)m.Invoke(reflectOb, obj))
                    {
                        System.Diagnostics.Debug.WriteLine("14 is between x and y");
                    }
                }
                else if (m.Name.Equals("Show", StringComparison.Ordinal))
                {
                    m.Invoke(reflectOb, null);
                }
                else if (m.Name.Equals("ReturnStr", StringComparison.Ordinal))
                {
                    System.Diagnostics.Debug.WriteLine(m.Invoke(reflectOb, null));
                }
            }
            #endregion

            TestAttribute();
            return View();
        }

        static void TestAttribute()
        {
            PrintAuthorInfo(typeof(AttributeClass));
            PrintAuthorInfo(typeof(AttributeClass1));
            PrintAuthorInfo(typeof(AttributeClass2));
            PrintAuthorInfo(typeof(AttributeClass3));
            PrintAuthorInfo(typeof(AttributeClass4));
        }
        /// <summary>
        /// 反射访问特性
        /// </summary>
        /// <param name="t"></param>
        private static void PrintAuthorInfo(System.Type t)
        {
            System.Diagnostics.Debug.WriteLine("MySpecialAttribute information for {0}", t);

            // Using reflection.  
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);  // Reflection.  

            // Displaying output.  
            foreach (System.Attribute attr in attrs)
            {
                if (attr is MySpecialAttribute)
                {
                    MySpecialAttribute a = (MySpecialAttribute)attr;
                    System.Diagnostics.Debug.WriteLine("   {0}, version {1:f}", a.GetName(), a.version);
                }
            }
        }
    }
    class MyClass
    {
        int x;
        int y;
        public MyClass(int i, int j)
        {
            this.x = i;
            this.y = j;
        }
        public int Sum()
        {
            return x + y;
        }
        public bool IsBetween(int i)
        {
            if (x < i && i < y)
            {
                return true;
            }
            return false;
        }
        //public void Set(int a, int b)
        //{
        //    x = a;
        //    y = b;
        //}
        //public void Set(double a, double b)
        //{
        //    x = (int)a;
        //    y = (int)b;
        //}

        public void Set(int a, int b)
        {
            System.Diagnostics.Debug.WriteLine("Set(int,int)");
            x = a;
            y = b;
            Show();
        }
        public void Set(double a, double b)
        {
            System.Diagnostics.Debug.WriteLine("Set(double,double)");
            x = (int)a;
            y = (int)b;
            Show();
        }
        public void Set(int a, string b)
        {
            System.Diagnostics.Debug.WriteLine("Set(int,string)");
            System.Diagnostics.Debug.WriteLine("x: " + a + "  y:  " + b);
        }

        public void Show()
        {
            System.Diagnostics.Debug.WriteLine("x: " + x + "  y:  " + y);
        }
        public string ReturnStr()
        {
            return "returnStr";
        }
    }

    #region 使用反射获取特性 GetCustomAttributes
    /// <summary>
    /// 新建一个特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)] // 设置特性的作用目标，作用于类或者方法,设置当前特性允许重复 [MySpecial("小明"), MySpecial("小虎队")] 
    public class MySpecialAttribute : Attribute
    {
        string name;
        public double version;
        public MySpecialAttribute(string name, double version = 1) // 特性构造函数
        {
            this.name = name;
            this.version = version;
        }
        public string GetName()
        {
            return name;
        }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)] // 仅作用于类,设置当前特性不允许重复 [MySpecialOther, MySpecialOther]编辑器会报错
    public class MySpecialOtherAttribute : Attribute
    {
    }
    //作用于测试特性的一些类
    /// <summary>
    /// 没有特性
    /// </summary>
    public class AttributeClass
    {
    }
    /// <summary>
    /// 使用特性
    /// </summary>
    [MySpecial("小明")]
    public class AttributeClass1
    {
    }
    /// <summary>
    /// 使用特性并带上版本
    /// </summary>
    [MySpecial("小虎队", version = 3)]
    public class AttributeClass2
    {
    }
    /// <summary>
    /// 两个特性，当前特性允许重复
    /// </summary>
    [MySpecial("小明"), MySpecial("小虎队", version = 3)] // 特性允许重复
    public class AttributeClass3
    {
    }
    /// <summary>
    /// 两个特性可以共存
    /// </summary>
    [MySpecial("小明"), MySpecialOther]
    public class AttributeClass4
    {
    }
    /// <summary>
    /// 两个特性 当前特性不允许重复
    /// </summary>
    //[MySpecialOther, MySpecialOther] 
    public class AttributeClass5
    {
    }
    #endregion
}



