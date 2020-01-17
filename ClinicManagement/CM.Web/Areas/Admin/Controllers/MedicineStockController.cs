using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CM.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class MedicineStockController : Controller
    {
        private readonly IMedicineStockService medicineStockService;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public MedicineStockController(IMedicineStockService medicineStockService)
        {
            this.medicineStockService = medicineStockService;       
        }
        public ActionResult GetMedicineStock()
        {
            try
            {
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var excelFile = medicineStockService.GetStockInExcel(out string fileName);
                return File(excelFile, contentType, fileName);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return null;
        }
    }
}