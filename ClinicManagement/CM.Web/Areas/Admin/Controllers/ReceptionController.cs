using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    public class ReceptionController : Controller
    {
        IReceptionService _receptionService;
        public ReceptionController(IReceptionService receptionService)
        {
            _receptionService = receptionService;
        }
        public ActionResult Appointment()
        {
            var getting = _receptionService.Get();
            return View();
        }

        public ActionResult ReceptionView()
        {
            return View();
        }
    }
}