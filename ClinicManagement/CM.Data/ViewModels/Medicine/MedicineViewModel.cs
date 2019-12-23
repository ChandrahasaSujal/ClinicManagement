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

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public double Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public double MRP { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public double OrderLevel { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public double StockLevel { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public Guid CategoryFK { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string ManufacturerName { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public Guid ManufacturerFk { get; set; }
        public string Description { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Manufacturers { get; set; }
    }
}
