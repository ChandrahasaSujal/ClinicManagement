using AutoMapper;
using CM.Data.Infrastructure;
using CM.Data.ViewModels.Billing;
using CM.Model.Models.Invoice;
using CM.Model.Models.Medicine;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private Invoice Invoice { get; set; }
        private List<PurchasedItem> PurchasedItems { get; set; }
        private InvoiceViewModel InvoiceViewModel { get; set; }
        private List<PurchasedItemViewModel> PurchasedItemViewModels { get; set; }

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public bool CreateInvoice(InvoiceViewModel order)
        {
            try
            {
                if (order!=null)
                {
                    Invoice = new Invoice();
                    Invoice.Id = Guid.NewGuid();
                    Invoice.CustomerFk = order.CustomerFk;

                    foreach (var item in order.PurchasedItems)
                    {
                        PurchasedItem purchasedItem = new PurchasedItem();
                        purchasedItem.Id = Guid.NewGuid();
                        purchasedItem.InvoiceFk = Invoice.Id;
                        purchasedItem.MedicineFk = item.MedicineFk;
                        purchasedItem.Quantity = item.Quantity;
                        purchasedItem.UnitPrice = item.UnitPrice;
                        unitOfWork.PurchasedItemRepository.Add(purchasedItem);

                        // update stock level of medicine
                        Medicine medicine = new Medicine();
                        medicine = unitOfWork.MedicineRepository.FirstOrDefault(m => m.Id == item.MedicineFk);
                        if (medicine!=null)
                        {
                            medicine.StockLevel = medicine.StockLevel - item.Quantity;
                            unitOfWork.MedicineRepository.Update(medicine);
                        }
                    }

                    unitOfWork.InvoiceRepository.Add(Invoice);
                    unitOfWork.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }
    }
}
