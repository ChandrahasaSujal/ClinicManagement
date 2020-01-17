using CM.Data.ViewModels.Medicine;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    public class ManufacturerController : Controller
    {
        private IManufacturerService manufacturerService;
        private bool isSuccess = false;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            this.manufacturerService = manufacturerService;       
        }

        [HttpPost]
        public JsonResult Add(ManufacturerViewModel manufacturer)
        {
            try
            {
                if (manufacturer != null)
                {
                    isSuccess = manufacturerService.AddManufacturer(manufacturer);
                    return Json(new { success = isSuccess, message = "Added Successfully!", JsonRequestBehavior.AllowGet });
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return Json(new { success = isSuccess, message = "Something goes Wrong!", JsonRequestBehavior.AllowGet });
        }
    }
}