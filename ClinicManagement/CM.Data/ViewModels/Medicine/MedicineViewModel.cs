using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CM.Data.ViewModels.Medicine
{
    public class MedicineViewModel : BaseViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public double UnitPirce { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public double MRP { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public double OrderLevel { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public SelectListItem SelectedCategory { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public SelectListItem SelectedManufacturrer { get; set; }
    }
}
