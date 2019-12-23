using CM.Data.ViewModels.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.ServiceInterfaces
{
    public interface IMedicineService
    {
        IEnumerable<MedicineViewModel> GetMedicines();
        bool AddMedicine(MedicineViewModel medicine);
        MedicineViewModel GetMedicine(Guid id);
        bool EditMedicine(MedicineViewModel medicine);
        bool DeleteMedicine(Guid id);
    }
}
