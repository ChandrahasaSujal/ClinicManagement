using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.ViewModels.Billing
{
    public class PurchasedItemViewModel 
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal MRP { get; set; }
        public Guid MedicineFk { get; set; }
        public string MedicineName { get; set; }
    }
}
