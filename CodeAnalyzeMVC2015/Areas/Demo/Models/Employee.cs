using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeAnalyzeMVC2015.Areas.Demo.Models
{
    #region 20185
    public class Employee
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public Employee()
        {

        }
        
        public Employee(string strId, string strName)
        {
            ID = strId;
            Name = strName;
        }
    }
    #endregion
    
    #region 20192
    public class MyViewModel
    {
        public int? BaseItem { get; set; }
        public int? ChildItem { get; set; }

        public IEnumerable<SelectListItem> BaseItems
        {
            get
            {

                List<SelectListItem> lstBaseItems = new List<SelectListItem>();
                lstBaseItems.Add(new SelectListItem
                {
                    Value = "Year",
                    Text = "Year"
                });
                lstBaseItems.Add(new SelectListItem
                {
                    Value = "Week",
                    Text = "Week"
                });
                return lstBaseItems;

            }
        }
    }
    #endregion

    #region 20194
    public class EmpRepository
    {
        public static IList<Employee> emps = null;

        public static IList<Employee> GetEmployees()
        {
            if (emps == null)
            {
                emps = new List<Employee>();
                emps.Add(new Employee() { ID = "1", Name = "Andy" });
                emps.Add(new Employee() { ID = "2", Name = "Alex" });
                emps.Add(new Employee() { ID = "3", Name = "Mike" });
                emps.Add(new Employee() { ID = "4", Name = "Lance" });
                emps.Add(new Employee() { ID = "5", Name = "Richard" });
                emps.Add(new Employee() { ID = "6", Name = "Jessica" });
                emps.Add(new Employee() { ID = "7", Name = "Bob" });
                emps.Add(new Employee() { ID = "8", Name = "Jeffery" });
                emps.Add(new Employee() { ID = "9", Name = "Henry" });
                emps.Add(new Employee() { ID = "10", Name = "Vlad" });
                emps.Add(new Employee() { ID = "11", Name = "Steve" });
                emps.Add(new Employee() { ID = "12", Name = "Mark" });
                emps.Add(new Employee() { ID = "13", Name = "Rubecca" });
                emps.Add(new Employee() { ID = "14", Name = "Lisa" });
                emps.Add(new Employee() { ID = "15", Name = "Susan" });
                emps.Add(new Employee() { ID = "16", Name = "Jared" });

            }
            return emps;
        }
    }  
    #endregion

}
