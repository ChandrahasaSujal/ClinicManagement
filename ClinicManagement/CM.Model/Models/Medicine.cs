using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models
{
    public class Medicine : BaseEntity
    {
        public int Id { get; set; }
        public string Drug { get; set; }
        public string Purpose { get; set; }
        public int OrderLevel { get; set; }
        public string BatchNo { get; set; }
        public DateTime MfgDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string Comments { get; set; }
    }
}
