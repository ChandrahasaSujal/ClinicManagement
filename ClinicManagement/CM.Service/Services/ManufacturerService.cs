using CM.Data.Infrastructure;
using CM.Data.ViewModels.Medicine;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.Services
{
    public class ManufacturerService : IManufacturerService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public ManufacturerService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public bool AddManufacturer(ManufacturerViewModel manufacturer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ManufacturerViewModel> GetManufacturers()
        {
            throw new NotImplementedException();
        }
    }
}
