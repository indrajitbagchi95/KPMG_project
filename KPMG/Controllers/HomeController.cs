using KPMG.Models;
using OfficeOpenXml;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;

namespace KPMG.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(string u, string p)
        {
            string m = "0";

            if (u == "admin" && p == "admin")
            {
                FormsAuthentication.SetAuthCookie(u, false);
                m = "1";

            }

            return Json(new { m }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IndexCustomer()
        {
            Customer obj = new Customer();
            obj.GetCustomerList();
            return View("IndexCustomer",obj);
        }
        public ActionResult GetCustomerDetailsAsExcel()
        {
            Customer objCustomer = new Customer();


            objCustomer.GetCustomerList();
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Customer_Sheet");


            using (ExcelRange Rng = Sheet.Cells["A1:E1"])
            {
                Rng.Style.Font.Size = 14;
                Rng.Style.Font.Bold = true;
            }
            Sheet.Cells["A1"].Value = "First Name";
            Sheet.Cells["B1"].Value = "Last Name";
            Sheet.Cells["C1"].Value = "City";
            Sheet.Cells["D1"].Value = "Country";
            Sheet.Cells["E1"].Value = "Phone";

            int row = 2;
            foreach (var item in objCustomer.CustomerList)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.FirstName;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.LastName;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.City;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.Country;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.Phone;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            byte[] arr = Ep.GetAsByteArray();

            string filename = "Customer" + ".xlsx";

            return File(arr, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
        public ActionResult GetCustomerChart()
        {
            ChartData obj = new ChartData();
            obj.GetChartData();
            return View("Charts", obj);
        }
    }
}