using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    public class ReceptionController : Controller
    {
        // GET: Admin/Reception
        public ActionResult Appointment()
        {
            return View();
        }

        public ActionResult ReceptionView()
        {
            return View();
        }
    }
}