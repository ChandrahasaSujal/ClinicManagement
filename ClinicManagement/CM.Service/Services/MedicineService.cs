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
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IEnumerable<Medicine> Medicines { get; set; }
        private IEnumerable<MedicineViewModel> MedicineViewModels { get; set; }
        private IEnumerable<Category> Categories;
        private IEnumerable<Manufacturer> Manufacturers;
        public MedicineService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            Categories = _unitOfWork.CategoryRepository.Fetch();
            Manufacturers = _unitOfWork.ManufacturerRepository.Fetch();
        }
        public IEnumerable<MedicineViewModel> GetMedicines()
        {
            try
            {
                Medicines = new List<Medicine>();
                MedicineViewModels = new List<MedicineViewModel>();
                Medicines = _unitOfWork.MedicineRepository.Fetch(m => m.IsDeleted == false);
                if (Medicines != null)
                {
                    MedicineViewModels = _mapper.Map(Medicines, MedicineViewModels);
                    foreach (var medicine in MedicineViewModels)
                    {
                        medicine.CategoryName = Categories.FirstOrDefault(c => c.Id == medicine.CategoryFK)?.CategoryName;
                        medicine.ManufacturerName = Manufacturers.FirstOrDefault(m => m.Id == medicine.ManufacturerFk)?.ManufacturerName;
                    }
                    return MedicineViewModels;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return null;
        }

        public bool AddMedicine(MedicineViewModel medicine)
        {
            try
            {
                if (medicine != null)
                {
                    medicine.Id = Guid.NewGuid();
                    var medicineDb = new Medicine();
                    medicineDb = _mapper.Map(medicine, medicineDb);
                    _unitOfWork.MedicineRepository.Add(medicineDb);
                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }
    }
}
