using AutoMapper;
using CM.Data.Infrastructure;
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

        public DashBoardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public int GetDayAppointments()
        {
            try
            {
                var todayDate = DateTime.Now;
                var NumberOfAppointments = (from People in unitOfWork.PeopleRepository.Fetch().ToList()
                                            where People.CreatedDate.Day == todayDate.Day
                                            && People.CreatedDate.Month == todayDate.Month
                                            && People.CreatedDate.Year == todayDate.Year
                                            select People).Count();
                return NumberOfAppointments;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int GetWeeklyAppointments()
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
            return 0;
        }

        public decimal GetWeeklySales()
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw;
            }
            return 0;
        }
    }
}
