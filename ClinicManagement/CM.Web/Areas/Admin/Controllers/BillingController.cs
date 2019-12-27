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
        private readonly IAppointmentService  appointmentService;
        bool isSuccess = false;

        public BillingController(IMedicineService medicineService,IAppointmentService appointmentService)
        {
            this.medicineService = medicineService;
            this.appointmentService = appointmentService;
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

        public JsonResult GetCustomer(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                var customer = appointmentService.GetCustomerByPhoneNumber(phoneNumber);
                if (customer != null)
                    return Json(new { success = true, customer = customer }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = false, message = "No Customer found for given Phone Number" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Something Goes Wrong, Please try again!" }, JsonRequestBehavior.AllowGet);
        }
    }
}