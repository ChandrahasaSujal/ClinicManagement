using CM.Data.Infrastructure;
using CM.Data.ViewModels.Appointment;
using CM.Model.Models;
using CM.Service.ServiceInterfaces;
using CM.Web.Common;
using NLog;
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
        private readonly IAppointmentService appointmentService;
        bool isSuccess = false;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        public ActionResult ViewAppointments()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAppointment(AppointmentViewModel appointment)
        {
            try
            {
                isSuccess = appointmentService.AddAppointment(appointment);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return Json(new { success = isSuccess, message = "Added successfully!" }, JsonRequestBehavior.AllowGet );
        }

        public ActionResult GetAppointments()
        {
            try
            {
                var appointmentData = appointmentService.GetAppointments();
                if (appointmentData != null)
                {
                    return Json(new { success = true, data = appointmentData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAppointment(string id)
        {
            try
            {
                if (id != null)
                {
                    var appointment = appointmentService.GetAppointment(id);
                    if (appointment != null)
                    {
                        return Json(new { success = true, appointee = appointment }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, message = "Something went Wrong!" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return Json(new { success = false, message = "Something went Wrong!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAppointment(AppointmentViewModel appointment)
        {
            try
            {
                if (appointment != null)
                {
                    isSuccess = appointmentService.UpdateAppointment(appointment);
                    return Json(new { success = true, message = "Updated Successfully!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return Json(new { success = false, message = "Error in Updating!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteAppointment(string appointmentId)
        {
            try
            {
                if (!string.IsNullOrEmpty(appointmentId) && Guid.TryParse(appointmentId, out Guid appointmentGuid))
                {
                    isSuccess = appointmentService.DeleteAppointment(appointmentGuid);
                    return Json(new { success = isSuccess, message = "Deleted Successfully!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return Json(new { success = isSuccess, message = "Something Went Wrong!" }, JsonRequestBehavior.AllowGet);
        }
    }
}