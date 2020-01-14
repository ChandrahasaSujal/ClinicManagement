using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.ViewModels.DashBoard
{
    public class DashBoardViewModel
    {
        public int DayAppointments { get; set; }
        public int WeeklyAppointments { get; set; }
        public decimal WeeklySales { get; set; }
    }
}
