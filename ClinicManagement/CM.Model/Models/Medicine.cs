using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public double UnitPirce { get; set; }
        [ForeignKey("CategoryId")]
        public virtual MedicineCategory MedicineCategory { get; set; }
    }
}
