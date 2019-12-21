using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CM.Data.ViewModels.Medicine
{
    public class MedicineViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPirce { get; set; }
        public CategoryViewModel SelectedCategory { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
