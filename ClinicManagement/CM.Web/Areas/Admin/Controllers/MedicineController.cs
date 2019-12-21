using CM.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class MedicineController : BaseController
    {
        // GET: Admin/Medicine
        public ActionResult Index()
        {
            return RedirectToAction("ViewMedicines");
        }

        public ActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNew(string a)
        {
            return View();
        }

        [ActionName("View")]
        public ActionResult ViewMedicines()
        {
            return View();
        }

    }
}