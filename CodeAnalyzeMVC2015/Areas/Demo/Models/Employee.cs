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

}
