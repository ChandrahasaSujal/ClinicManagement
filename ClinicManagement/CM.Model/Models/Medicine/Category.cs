using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models.Medicine
{
    public class Category : BaseEntity
    {
        [Required]
        public string CategoryName { get; set; }
        public string CatrgoryDescription { get; set; }
    }
}
