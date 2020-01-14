using CM.Data.ViewModels.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.ServiceInterfaces
{
    public interface IDashBoardService
    {
        DashBoardViewModel GetDataForDashBoard();
    }
}
