using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models.Medicine
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPirce { get; set; }
        public Guid CategoryFk { get; set; }
        public Guid ManufacturerFk { get; set; }
        [ForeignKey("CategoryFk")]
        public virtual Category MedicineCategory { get; set; }
        [ForeignKey("ManufacturerFk")]
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
