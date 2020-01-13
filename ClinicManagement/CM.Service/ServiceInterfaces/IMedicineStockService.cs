using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CM.Service.ServiceInterfaces
{
    public interface IMedicineStockService
    {
        byte[] GetStockInExcel(out string fileName);
    }
}
