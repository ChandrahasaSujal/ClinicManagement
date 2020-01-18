using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.ViewModels.Billing
{
    public class InvoiceViewModel : BaseViewModel
    {
        public Guid CustomerFk { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<PurchasedItemViewModel> PurchasedItems { get; set; }
    }
}
