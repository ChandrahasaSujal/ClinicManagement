using CM.Data.ViewModels.Billing;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IMedicineService medicineService;
        private readonly IAppointmentService appointmentService;
        private readonly IInvoiceService invoiceService;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public InvoiceController(IMedicineService medicineService, IAppointmentService appointmentService, IInvoiceService invoiceService)
        {
            this.medicineService = medicineService;
            this.appointmentService = appointmentService;
            this.invoiceService = invoiceService;
        }
        // GET: Admin/Invoice
        public ActionResult Index()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return View();
        }

        public ActionResult Create()
        {
            try
            {

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
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
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return Json(new { success = true, message = "Something went wrong Please try again!" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomer(string phoneNumber)
        {
            try
            {
                if (!string.IsNullOrEmpty(phoneNumber))
                {
                    var customer = appointmentService.GetCustomerByPhoneNumber(phoneNumber);
                    if (customer != null)
                        return Json(new { success = true, customer = customer }, JsonRequestBehavior.AllowGet);
                    else
                        return Json(new { success = false, message = "No Customer found for given Phone Number" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return Json(new { success = false, message = "Something Goes Wrong, Please try again!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateInvoice( InvoiceViewModel order )
        {
            try
            {
               var invoiceId = invoiceService.CreateInvoice(order);
                if (invoiceId!=Guid.Empty)
                {
                    return Json(new { success = true, invoiceId = invoiceId }, JsonRequestBehavior.AllowGet);
                }
                else
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ShowInvoice(string invoiceId)
        {
            try
            {
                if (Guid.TryParse(invoiceId, out Guid invoiceGuid))
                {
                    var invoice = invoiceService.GetInvoice(invoiceGuid);
                    return View(invoice);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return null;
        }

        [HttpGet]
        public ActionResult PrintInvoice(Guid invoiceId)
        {
            try
            {
                var invoice = invoiceService.GetInvoice(invoiceId);
                return View(invoice);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
                return null;
            }
        }
    }
}