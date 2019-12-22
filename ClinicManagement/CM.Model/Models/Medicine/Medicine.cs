using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models.Medicine
{
    public class Medicine : BaseEntity
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(AllowEmptyStrings = false)]
        public double Price { get; set; }
        [Required(AllowEmptyStrings = false)]
        public double MRP { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int OrderLevel { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int StockLevel { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Guid CategoryFk { get; set; }
        [Required(AllowEmptyStrings = false)]
        public Guid ManufacturerFk { get; set; }
        [ForeignKey("CategoryFk")]
        public virtual Category MedicineCategory { get; set; }
        [ForeignKey("ManufacturerFk")]
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
