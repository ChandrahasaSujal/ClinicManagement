﻿using CM.Data.ViewModels.Billing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.ServiceInterfaces
{
    public interface IInvoiceService
    {
        Guid CreateInvoice(InvoiceViewModel order);
        InvoiceViewModel GetInvoice(Guid invoiceId);
    }
}
