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
        private readonly IMedicineService medicineService;
        private readonly ICategoryService categoryService;
        private readonly IManufacturerService manufacturerService;
        private bool isSucces = false;
        public MedicineController(IMedicineService medicineService, ICategoryService categoryService, IManufacturerService manufacturerService)
        {
            this.medicineService = medicineService;
            this.categoryService = categoryService;
            this.manufacturerService = manufacturerService;
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
                GetDropDownData();
                ViewBag.SubTitle = "Add New";
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        private void GetDropDownData()
        {
            try
            {
                TempData["Categories"] = categoryService.GetCategoriesForDropDownList();
                TempData["Manufacturers"] = manufacturerService.GetManufacturersForDropDownList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult AddNew(MedicineViewModel medicine)
        {
            try
            {
                if (medicine.Id == Guid.Empty)
                {
                    medicineService.AddMedicine(medicine);
                }
                if (medicine.Id !=Guid.Empty)
                {
                    isSucces = medicineService.EditMedicine(medicine);
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
                var medicines = medicineService.GetMedicines();
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

        [HttpGet]
        public ActionResult Update(Guid id)
        {
            if (id != null)
            {
                GetDropDownData();
                ViewBag.SubTitle = "Update";
                ViewBag.Layout = "~/Areas/Admin/Views/Shared/_Layout.cshtml";

                var medicine = medicineService.GetMedicine(id);
                return PartialView("_AddNewMedicine", medicine);
            }
            return RedirectToAction("View");
        }

        public JsonResult Delete(Guid id)
        {
            try
            {
                if (id != null)
                {
                    isSucces = medicineService.DeleteMedicine(id);
                    return Json(new { success = isSucces, message = "Deleted Successfully!" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Json(new { success = isSucces, message = "Deleted Successfully!" });
        }
    }
}