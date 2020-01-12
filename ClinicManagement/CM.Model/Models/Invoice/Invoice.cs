using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models.Invoice
{
    public class Invoice : BaseEntity
    {
        public Guid CustomerFk { get; set; }
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string InvoiceNumber { get; set; }

        [ForeignKey("CustomerFk")]
        public virtual Person Person { get; set; }
    }
}
