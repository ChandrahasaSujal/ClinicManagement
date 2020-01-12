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
        private PurchasedItem PurchasedItem { get; set; }
        private InvoiceViewModel InvoiceViewModel { get; set; }
        private List<PurchasedItemViewModel> PurchasedItemViewModels { get; set; }

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public Guid CreateInvoice(InvoiceViewModel order)
        {
            try
            {
                if (order!=null)
                {
                    Invoice = new Invoice();
                    Invoice.Id = Guid.NewGuid();
                    var dateTimeNow = DateTime.Now.ToString("yyMMddHHmmssfff");
                    Invoice.InvoiceNumber = dateTimeNow + DateTime.Now.Day;
                    Invoice.CustomerFk = order.CustomerFk;

                    foreach (var item in order.PurchasedItems)
                    {
                        PurchasedItem purchasedItem = new PurchasedItem();
                        purchasedItem.Id = Guid.NewGuid();
                        
                        purchasedItem.InvoiceFk = Invoice.Id;
                        purchasedItem.MedicineFk = item.MedicineFk;
                        purchasedItem.Quantity = item.Quantity;
                        purchasedItem.UnitPrice = item.UnitPrice;
                        purchasedItem.MRP = item.MRP;
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
                    return Invoice.Id;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Guid.Empty;
        }

        public InvoiceViewModel GetInvoice(Guid invoiceId)
        {
            try
            {
                Invoice = new Invoice();
                Invoice = unitOfWork.InvoiceRepository.FirstOrDefault(p => p.Id == invoiceId);
                PurchasedItems = unitOfWork.PurchasedItemRepository.Fetch(p=>p.InvoiceFk==Invoice.Id).ToList();
                return GetInvoiceViewModel(Invoice, PurchasedItems);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private InvoiceViewModel GetInvoiceViewModel(Invoice invoice, List<PurchasedItem> purchasedItems)
        {
            try
            {
                InvoiceViewModel invoiceViewModel = new InvoiceViewModel();
                invoiceViewModel = mapper.Map(invoice, invoiceViewModel);
                List<PurchasedItemViewModel> purchasedItemsViewModels = new List<PurchasedItemViewModel>();

                purchasedItemsViewModels = mapper.Map(purchasedItems, purchasedItemsViewModels);
                invoiceViewModel.PurchasedItems = mapper.Map(purchasedItems, purchasedItemsViewModels);
                foreach (var item in invoiceViewModel.PurchasedItems)
                {
                    item.MedicineName = unitOfWork.MedicineRepository.FirstOrDefault(m=>m.Id==item.MedicineFk).Name;
                    decimal subTotal = item.Quantity * item.UnitPrice;
                    invoiceViewModel.Total += subTotal;
                }
                return invoiceViewModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
