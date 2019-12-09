using CM.Data.Infrastructure;
using CM.Data.ViewModels.Appointment;
using CM.Model.Models;
using CM.Service.ServiceInterfaces;
using CM.Tools.Enums;
using CM.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class AppointmentController : BaseController
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            this._appointmentService = appointmentService;
        }
        public ActionResult ViewAppointments()
        {
            return View();
        }

        public ActionResult AddAppointment(int id=0)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAppointment(AppointmentViewModel appointment)
        {
            if (ModelState.IsValid)
            {
              var isSuccess = _appointmentService.AddAppointment(appointment);
              return Json(new { success = isSuccess, message = "Added successfully!",JsonRequestBehavior.AllowGet});
            }
            return Json(new { success = false, message = "Error while Adding!", JsonRequestBehavior.AllowGet });
        }

        public ActionResult GetAppointments()
        {
            try
            {
                var appointmentData = _appointmentService.GetAppointments();
                
                return Json(new { data =  appointmentData},JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}