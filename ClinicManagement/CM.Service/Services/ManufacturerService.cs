using AutoMapper;
using CM.Data.Infrastructure;
using CM.Data.ViewModels.Medicine;
using CM.Model.Models.Medicine;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        IEnumerable<Manufacturer> Manufacturers;
        IEnumerable<ManufacturerViewModel> ManufacturerViewModels;
        public ManufacturerService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool AddManufacturer(ManufacturerViewModel manufacturer)
        {
            Manufacturer manufacturerDb;
            try
            {
                if (manufacturer != null)
                {
                    manufacturerDb = new Manufacturer();
                    manufacturerDb = _mapper.Map(manufacturer, manufacturerDb);
                    _unitOfWork.ManufacturerRepository.Add(manufacturerDb);
                    _unitOfWork.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }

        public IEnumerable<ManufacturerViewModel> GetManufacturers()
        {
            try
            {
                Manufacturers = new List<Manufacturer>();
                ManufacturerViewModels = new List<ManufacturerViewModel>();
                Manufacturers = _unitOfWork.ManufacturerRepository.Fetch(c => c.IsDeleted == false);
                if (Manufacturers != null)
                {
                    ManufacturerViewModels = _mapper.Map(Manufacturers, ManufacturerViewModels);
                    return ManufacturerViewModels;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
    }
}
