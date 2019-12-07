using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    public class AppointmentController : Controller
    {
        public ActionResult ViewAppointments()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddAppointment()
        {
            return View("ViewAppointments");
        }
    }
}