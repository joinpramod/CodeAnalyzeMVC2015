using CodeAnalyzeMVC2015.Areas.Demo.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace CodeAnalyzeMVC2015.Areas.Demo.Controllers
{
    public class CodeController : Controller
    {
        //
        // GET: /CodeDemos/

        public ActionResult Articles(string strId)
        {
            string articleId = string.Empty;
            if (RouteData.Values.Count > 0 && RouteData.Values["Id"] != null)
                articleId = RouteData.Values["Id"].ToString();
            else
                articleId = strId;

            switch(articleId)
            {
                case "20183":
                    #region 20183
                    if (articleId.Equals("20183"))
                    {
                        if (Request.Form["btnSave"] != null)
                        {
                            ViewBag.DemoMessage = "Save Clicked";
                        }

                        if (Request.Form["btnCancel"] != null)
                        {
                            ViewBag.DemoMessage = "Cancel clicked";
                        }
                    }
                    #endregion
                    break;

                case "20184":
                    #region 20184
                    if (articleId.Equals("20184"))
                    {
                        if (Request.Form["txtBoxes"] != null)
                        {
                            ViewBag.DemoMessage = Request.Form["txtBoxes"];
                        }
                    }
                    #endregion
                    break;

                case "20185":
                    #region 20185
                    if (articleId.Equals("20185"))
                    {
                        return GetEmployees(0);
                    }
                    #endregion
                    break;

                case "20186":
                    #region 20186
                    if (articleId.Equals("20186"))
                    {
                        if (Request.Form["lbEmp"] != null)
                        {
                            string lbEmp = Request.Form["lbEmp"];
                            ViewBag.Message += lbEmp;
                        }
                        List<SelectListItem> items = GetItems20186();
                        return View("../Code/" + articleId, items);
                    }
                    #endregion
                    break;

                case "20187":
                    #region 20187
                    if (articleId.Equals("20187"))
                    {
                        if (Request.Form["BarChart"] != null)
                        {
                            ViewBag.Message = "Bar";
                        }

                        if (Request.Form["PieChart"] != null)
                        {
                            ViewBag.Message = "Pie";
                        }

                        if (Request.Form["LineChart"] != null)
                        {
                            ViewBag.Message = "Line";
                        }
                    }
                    #endregion
                    break;

                case "20189":
                    #region 20189
                    if (articleId.Equals("20189"))
                    {
                        if (Request.Form["hiddenValue"] != null)
                        {
                            if (Request.Form["hiddenValue"] == "Yes")
                            {
                                ViewBag.Message = "OK";
                            }
                            else
                            {
                                ViewBag.Message = "Cancel";
                            }
                        }
                    }
                    #endregion
                    break;
                    
                case "20192":
                    #region 20192
                    MyViewModel model = new MyViewModel();
                    return View("../Code/20192", model);
                    #endregion
                    
                case "20194":
                    #region 20194
                    return View("../Code/20194", EmpRepository.GetEmployees());
                    #endregion
            }
            return View("../Code/" + articleId);
        }
        
        
        #region 20185-api-type-req lazy loading
        public const int RecordsPerPage = 5;
        public List<Employee> EmployeeData;


        public ActionResult GetEmployees(int? pageNum)
        {
            pageNum = pageNum ?? 0;
            ViewBag.IsEndOfRecords = false;
            if (Request.IsAjaxRequest())
            {
                var employees = GetRecordsForPage(pageNum.Value);
                ViewBag.IsEndOfRecords = (employees.Any());
                return PartialView("_EmployeeData", employees);
            }
            else
            {
                EmployeeData = GetEmployeeList();

                ViewBag.TotalNumberEmployees = EmployeeData.Count;
                ViewBag.Employees = GetRecordsForPage(pageNum.Value);

                return View("20185");
            }
        }

        public List<Employee> GetRecordsForPage(int pageNum)
        {
            EmployeeData = GetEmployeeList();
            int fromRecords = (pageNum * RecordsPerPage);
            var tempList = (from rec in EmployeeData select rec).Skip(fromRecords).Take(20).ToList<Employee>();
            return tempList;
        }


        public List<Employee> GetEmployeeList()
        {
            //string employeeFile = HostingEnvironment.MapPath("~/App_Data/Employees.txt");
            List<Employee> tempList = new List<Employee>();
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));
            tempList.Add(new Employee("1000", "Employee-1000"));
            tempList.Add(new Employee("1001", "Employee-1001"));
            tempList.Add(new Employee("1002", "Employee-1002"));
            tempList.Add(new Employee("1073", "Employee-1073"));
            tempList.Add(new Employee("1074", "Employee-1074"));


            return tempList;
        }
        #endregion
      
      
        #region 20186-api-type-req dropdown with checkbox bind dd initially
        private static List<SelectListItem> GetItems20186()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem
            {
                Text = "Microsoft",
                Value = "1"
            });

            items.Add(new SelectListItem
            {
                Text = "Apple",
                Value = "2"
            });

            items.Add(new SelectListItem
            {
                Text = "IBM",
                Value = "3"
            });

            items.Add(new SelectListItem
            {
                Text = "Oracle",
                Value = "4"
            });

            items.Add(new SelectListItem
            {
                Text = "Google",
                Value = "5"
            });
            return items;
        }
        #endregion

        
        #region 20191-api-type-req pass model from jquery
        [HttpPost]
        public JsonResult PassModelFromJQuery(Employee emp)
        {
            return Json(emp);
        }
        #endregion
        
        
        #region 20192-api-type-req AJ cascade dropdownlist

        public ActionResult GetChildItems(string baseItem)
        {
            if (baseItem == "Year")
            {
                List<SelectListItem> lstChildItems = new List<SelectListItem>();
                lstChildItems.Add(new SelectListItem { Value = "Jan", Text = "Jan" });
                lstChildItems.Add(new SelectListItem { Value = "Feb", Text = "Feb" });
                lstChildItems.Add(new SelectListItem { Value = "March", Text = "March" });
                lstChildItems.Add(new SelectListItem { Value = "April", Text = "April" });
                lstChildItems.Add(new SelectListItem { Value = "May", Text = "May" });
                lstChildItems.Add(new SelectListItem { Value = "June", Text = "June" });
                return Json(lstChildItems, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<SelectListItem> lstChildItems = new List<SelectListItem>();
                lstChildItems.Add(new SelectListItem { Value = "Sunday", Text = "Sunday" });
                lstChildItems.Add(new SelectListItem { Value = "Monday", Text = "Monday" });
                lstChildItems.Add(new SelectListItem { Value = "Tuesday", Text = "Tuesday" });
                lstChildItems.Add(new SelectListItem { Value = "Wednesday", Text = "Wednesday" });
                lstChildItems.Add(new SelectListItem { Value = "Thrusday", Text = "Thrusday" });
                lstChildItems.Add(new SelectListItem { Value = "Friday", Text = "Friday" });
                lstChildItems.Add(new SelectListItem { Value = "Saturday", Text = "Saturday" });
                return Json(lstChildItems, JsonRequestBehavior.AllowGet);

            }
        }

        #endregion



    }



}
