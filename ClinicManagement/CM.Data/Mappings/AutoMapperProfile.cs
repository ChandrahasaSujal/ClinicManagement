using AutoMapper;
using CM.Data.ViewModels.Appointment;
using CM.Data.ViewModels.Billing;
using CM.Data.ViewModels.Medicine;
using CM.Model.Models;
using CM.Model.Models.Invoice;
using CM.Model.Models.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.Mappings
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Person, AppointmentViewModel>().ReverseMap();
            CreateMap<Medicine, MedicineViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Manufacturer, ManufacturerViewModel>().ReverseMap();
            CreateMap<Invoice, InvoiceViewModel>().ReverseMap();
            CreateMap<PurchasedItem, PurchasedItemViewModel>().ReverseMap();
        }
    }
}
