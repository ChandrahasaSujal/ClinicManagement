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
            //IEnumerable<AppointmentViewModel> appointments;
            //IEnumerable<Patient> patients;
            //patients = _appointmentService.GetAppointments();
            return View();
        }

        public ActionResult AddAppointment(int id=0)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAppointment()
        {
            return View("ViewAppointments");
        }

        public ActionResult GetAppointments()
        {
            try
            {
                Patient patient = new Patient()
                {
                    Name = "Chandu",
                    DOA = DateTime.Now,
                    MailId = "test@gmail.com",
                    Phone = "95869875665",
                    Gender = Gender.Male
                };
                return Json(new { data = patient }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}