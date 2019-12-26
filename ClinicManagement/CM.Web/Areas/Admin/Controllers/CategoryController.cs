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
    public class CategoryController : BaseController
    {
        private ICategoryService categoryService;
        private bool isSuccess = false; 
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        
        [HttpPost]
        public JsonResult Add(CategoryViewModel category) 
        {
            try
            {
                if (category != null)
                {
                    isSuccess = categoryService.AddCategory(category);
                    return Json(new { success = isSuccess, message = "Added Successfully!", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Json(new { success = isSuccess, message = "Something goes Wrong!", JsonRequestBehavior.AllowGet });
        }
    }
}