using AutoMapper;
using CM.Data.ViewModels.Appointment;
using CM.Model.Models;
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
        }
    }
}
