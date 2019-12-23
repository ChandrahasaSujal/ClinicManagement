using CM.Data.ViewModels.Medicine;
using CM.Service.ServiceInterfaces;
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
        private readonly IMedicineService _medicineService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        public MedicineController(IMedicineService medicineService, ICategoryService categoryService,IManufacturerService manufacturerService)
        {
            _medicineService = medicineService;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
        }
        // GET: Admin/Medicine
        public ActionResult Index()
        {
            return RedirectToAction("View");
        }

        public ActionResult AddNew()
        {
            try
            {
                TempData["Categories"] = _categoryService.GetCategoriesForDropDownList();
                TempData["Manufacturers"] = _manufacturerService.GetManufacturersForDropDownList();
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddNew(MedicineViewModel medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _medicineService.AddMedicine(medicine);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("View");
        }

        [ActionName("View")]
        public ActionResult ViewMedicines()
        {
            return View();
        }

        public JsonResult GetMedicines()
        {
            try
            {
                var medicines = _medicineService.GetMedicines();
                if (medicines != null)
                {
                    return Json(new { success = true, data = medicines }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

            }
            return Json(new { success = true, message = "Something went wrong Please try again!" }, JsonRequestBehavior.AllowGet);
        }

    }
}