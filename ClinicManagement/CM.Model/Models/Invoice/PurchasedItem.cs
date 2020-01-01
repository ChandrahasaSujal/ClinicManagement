using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models.Invoice
{
    public class PurchasedItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid MedicineFk { get; set; }
        public Guid InvoiceFk { get; set; }

        [ForeignKey("MedicineFk")]
        public virtual Medicine.Medicine Medicine { get; set; }

        [ForeignKey("InvoiceFk")]
        public virtual Invoice Invoice { get; set; }

}
}
