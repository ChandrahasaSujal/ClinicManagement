using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ReceptionController : Controller
    {
        IReceptionService _receptionService;
        public ReceptionController(IReceptionService receptionService)
        {
            _receptionService = receptionService;
        }
        public ActionResult AddAppointment()
        {
            var getting = _receptionService.Get();
            return View();
        }

        public ActionResult ViewAppointments()
        {
            return View();
        }
    }
}