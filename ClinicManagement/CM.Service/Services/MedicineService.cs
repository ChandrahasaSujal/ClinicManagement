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
        public MedicineService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                    return MedicineViewModels;
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
