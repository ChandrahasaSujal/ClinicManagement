using AutoMapper;
using CM.Data.Infrastructure;
using CM.Data.ViewModels.DashBoard;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DashBoardViewModel DashBoard { get; set; }

        public DashBoardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public DashBoardViewModel GetDataForDashBoard()
        {
            DashBoard = new DashBoardViewModel();
            try
            {
                
                var todayDate = DateTime.Now;
                var appointmentsList = unitOfWork.PeopleRepository.Fetch().ToList();
                DashBoard.DayAppointments = (from people in appointmentsList
                                             where people.CreatedDate.Day == todayDate.Day
                                             && people.CreatedDate.Month == todayDate.Month
                                             && people.CreatedDate.Year == todayDate.Year
                                             select people).Count();

                DashBoard.WeeklyAppointments = (from people in appointmentsList
                                                where people.CreatedDate >= DateTime.Now.AddDays(-7)
                                                select people).Count();


                DashBoard.WeeklySales = (from purchases in unitOfWork.PurchasedItemRepository.Fetch().ToList()
                                         where purchases.CreatedDate >= DateTime.Now.AddDays(-7)
                                         select purchases.Quantity * purchases.UnitPrice).Sum();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return DashBoard;
        }
    }
}
