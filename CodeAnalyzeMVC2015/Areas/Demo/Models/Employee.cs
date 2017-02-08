using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}
