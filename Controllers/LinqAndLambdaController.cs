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
    public class LinqAndLambdaController : Controller
    {
        public ActionResult Index()
        {
            LinkAndLambda();
            return View();
        }
        public void LinkAndLambda()
        {
            #region 1 普通筛选 lambda表达式更加优雅
            //构造数据
            List<CustomerModel> customerlist = new List<CustomerModel> { };
            int i = 0;
            while (i < 10)
            {
                customerlist.Add(new CustomerModel { Id = i, Name = i + "哥", Age = i });
                i++;
            }
            //linq 查询表达式
            var linqcustomer = from customer in customerlist
                               where (customer.Age == 1 || customer.Age == 5)
                               select new { customer.Id, customer.Name, customer.Age }; // 此句不可省略否则报错

            //lambda 点表达式
            var lambdacustomer = customerlist.Where(customer => customer.Age == 1 || customer.Age == 5).ToList();

            var lambdacustomer1 = customerlist.Where(customer => customer.Age == 1 || customer.Age == 5)
                .Reverse() // 逆序
                .Distinct() //去重
                .Except(customerlist) // 差集
                .Union(customerlist) // 并集
                .Intersect(customerlist) // 交集
                .ToList();
            #endregion

            #region 2 查询两个列表中Id相等的值，多字段排序 linq表达式更加方便直接
            //var list1 = new List<CustomerModel> {
            //    new CustomerModel { Id=1,Name="张三"},
            //    new CustomerModel { Id=2,Name = "李四"},
            //    new CustomerModel { Id=3,Name="张三"},
            //    new CustomerModel { Id=4,Name = "小米"},
            //};
            //var list2 = new List<CustomerModel> {
            //    new CustomerModel { Id=1,Name="张三"},
            //    new CustomerModel { Id=2,Name = "李四"},
            //    new CustomerModel { Id=3,Name="张三"},
            //    new CustomerModel { Id=5,Name = "小米"},
            //};
            //// linq 查询表达式
            //var obj1 = from l1 in list1
            //           join l2 in list2
            //           on l1.Id equals l2.Id
            //           orderby l1.Id, l2.Id descending // 多字段不同方式排序
            //           select new { l1, l2 };
            //foreach (var item in obj1)
            //{
            //    System.Diagnostics.Debug.WriteLine(item.l1.Name.ToString() + item.l2.Name.ToString());
            //}

            //// lambda 点标记
            //var obj = list1.Join(list2, l1 => l1.Id, l2 => l2.Id, (l1, l2) => new { l1, l2 })
            //    .OrderBy(li => li.l1.Id) // 多字段不同方式排序 需要分开写
            //    .ThenByDescending(li => li.l2.Id).ToList(); // 多字段不同方式排序
            //foreach (var item in obj)
            //{
            //    System.Diagnostics.Debug.WriteLine(item.l1.Name.ToString() + item.l2.Name.ToString());
            //}
            #endregion

            #region 3.联接查询（内联、左联接、交叉联接）
            //内联查询
            var list1 = new Dictionary<int, string> { { 1, "小红" }, { 2, "小兰" } };
            var list2 = new Dictionary<int, string> { { 1, "小红" }, { 3, "小何" } };
            var linqnei = from l1 in list1
                          join l2 in list2
                          on l1.Key equals l2.Key
                          select new { l1, l2 };
            foreach (var item in linqnei)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
            var lambdanei = list1.Join(list2, l1 => l1.Key, l2 => l2.Key, (l1, l2) => new { l1, l2 }).ToList();
            foreach (var item in lambdanei)
            {
                System.Diagnostics.Debug.WriteLine(item);
            }
           // // 左联接
           //var linqleft = from l1 in list1
           //               join l2 in list2
           //               on l1.Key equals l2.Key into list
           //               from l2 in list.DefaultIfEmpty()
           //               select new { l1, l2 };
           // foreach (var item in linqleft)
           // {
           //     System.Diagnostics.Debug.WriteLine(item);
           // }
           // var lambdaleft = list1.GroupJoin(list2, l1 => l1.Key, l2 => l2.Key, (l1, l2) => new { l1, l2 = l2.FirstOrDefault() }).ToList();
           // foreach (var item in lambdaleft)
           // {
           //     System.Diagnostics.Debug.WriteLine(item);
           // }
           // // 交叉联接
           //var linq = from l1 in list1
           //           from l2 in list2
           //           select new { l1, l2 };
           // foreach (var item in linq)
           // {
           //     System.Diagnostics.Debug.WriteLine(item);
           // }
           // var lambda = list1.SelectMany(l1 => list2.Select(l2 => new { l1, l2 }));
           // foreach (var item in lambda)
           // {
           //     System.Diagnostics.Debug.WriteLine(item);
           // }
            #endregion
        }
    }
    public class CustomerModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}



