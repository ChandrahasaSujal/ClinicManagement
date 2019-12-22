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
            return RedirectToAction("ViewMedicines");
        }

        public ActionResult AddNew()
        {
            try
            {
                var categoryList = _categoryService.GetCategories();
                var listCategory = new List<SelectListItem>();
                foreach (var category in categoryList)
                {
                    var categoryItem = new SelectListItem();
                    categoryItem.Text = category.CategoryName;
                    categoryItem.Value = category.Id.ToString();
                    listCategory.Add(categoryItem);
                }
                ViewBag.CategoryList = listCategory;

                var manufacturerList = _manufacturerService.GetManufacturers();
                var listmanufacturer = new List<SelectListItem>();
                foreach (var manufacturer in manufacturerList)
                {
                    var manufacturerItem = new SelectListItem();
                    manufacturerItem.Text = manufacturer.ManufacturerName;
                    manufacturerItem.Value = manufacturer.Id.ToString();
                    listmanufacturer.Add(manufacturerItem);
                }
                ViewBag.ManufacturerList = listmanufacturer;
            }
            catch (Exception)
            {

                throw;
            }
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