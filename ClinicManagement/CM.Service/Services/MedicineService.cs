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
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private IEnumerable<Medicine> Medicines { get; set; }
        private IEnumerable<MedicineViewModel> MedicineViewModels { get; set; }
        private IEnumerable<Category> Categories;
        private IEnumerable<Manufacturer> Manufacturers;
        private MedicineViewModel MedicineViewModel { get; set; }
        private Medicine Medicine { get; set; }
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
                Medicines = _unitOfWork.MedicineRepository.Fetch();
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
                logger.Error(ex, "Something bad happened");
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
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return false;
        }

        public MedicineViewModel GetMedicine(Guid id)
        {
            MedicineViewModel = new MedicineViewModel();
            try
            {
                if (id != null)
                {
                    Medicine = _unitOfWork.MedicineRepository.FirstOrDefault(m => m.Id == id && m.IsDeleted == false);
                    MedicineViewModel = _mapper.Map(Medicine, MedicineViewModel);
                    return MedicineViewModel;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return null;
        }

        public bool EditMedicine(MedicineViewModel medicine)
        {
            try
            {
                if (medicine != null)
                {
                    Medicine = new Medicine();
                    Medicine = _unitOfWork.MedicineRepository.FirstOrDefault(m => m.Id == medicine.Id);
                    Medicine = _mapper.Map(medicine, Medicine);
                    if (Medicine != null)
                    {
                        _unitOfWork.MedicineRepository.Update(Medicine);
                        _unitOfWork.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return false;
        }

        public bool DeleteMedicine(Guid id)
        {
            try
            {
                if (id!=null)
                {
                    _unitOfWork.MedicineRepository.LogicalDelete(id);
                    _unitOfWork.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Something bad happened");
            }
            return false;
        }
    }
}
