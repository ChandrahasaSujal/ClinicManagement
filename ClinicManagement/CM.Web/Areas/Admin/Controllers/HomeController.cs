using CM.Data.Infrastructure;
using CM.Data.ViewModels.DashBoard;
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
    public class HomeController : BaseController
    {
        private readonly IDashBoardService dashBoardService;
        public HomeController(IDashBoardService dashBoardService)
        {
            this.dashBoardService = dashBoardService;
        }

        public ActionResult Index()
        {
            var dashBoard = dashBoardService.GetDataForDashBoard();

            return View(dashBoard);
        }
    }
}