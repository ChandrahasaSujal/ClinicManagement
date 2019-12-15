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

        [HttpPost]
        public ActionResult AddAppointment(AppointmentViewModel appointment)
        {
            var isSuccess = false;
            try
            {
                isSuccess = _appointmentService.AddAppointment(appointment);
            }
            catch (Exception)
            {

            }
            return Json(new { success = isSuccess, message = "Added successfully!", JsonRequestBehavior.AllowGet });
        }

        public ActionResult GetAppointments()
        {
            try
            {
                var appointmentData = _appointmentService.GetAppointments();

                return Json(new { data = appointmentData }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult EditAppointment(string id)
        {
            if (id != null)
            {
                var appointment =_appointmentService.GetAppointment(id);
                if (appointment != null)
                {
                    return Json(new { success = true, appointee = appointment }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditAppointment(AppointmentViewModel appointment)
        {
            if (appointment!=null)
            {
                _appointmentService.EditAppointment(appointment);
            }
            return View();
        }
    }
}