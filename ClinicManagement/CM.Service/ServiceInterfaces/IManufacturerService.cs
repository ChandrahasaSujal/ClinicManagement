using CM.Data.ViewModels.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.ServiceInterfaces
{
    public interface IManufacturerService
    {
        bool AddManufacturer(ManufacturerViewModel manufacturer);
        IEnumerable<ManufacturerViewModel> GetManufacturers();
    }
}
