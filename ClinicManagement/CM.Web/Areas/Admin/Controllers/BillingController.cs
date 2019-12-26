using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    public class BillingController : Controller
    {
        private readonly IMedicineService  medicineService;
        bool isSuccess = false;

        public BillingController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
        }
        // GET: Admin/Billing
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public JsonResult GetMedicines()
        {
            try
            {
                var medicines = medicineService.GetMedicines();
                if (medicines != null)
                {
                    return Json(new { success = true, data = medicines }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

            }
            return Json(new { success = true, message = "Something went wrong Please try again!" }, JsonRequestBehavior.AllowGet);
        }
    }
}