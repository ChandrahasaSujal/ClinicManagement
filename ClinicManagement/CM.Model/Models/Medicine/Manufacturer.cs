using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models.Medicine
{
    public class Manufacturer : BaseEntity
    {
        [Required]
        public string ManufacturerName { get; set; }
        public string ManufacturerDescription { get; set; }
    }
}
