using CM.Data.ViewModels.Appointment;
using CM.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.ServiceInterfaces
{
    public interface IAppointmentService
    {
       IEnumerable<AppointmentViewModel> GetAppointments();
       AppointmentViewModel GetAppointment(string appointmentId);
       bool AddAppointment(AppointmentViewModel appointment);
       bool UpdateAppointment(AppointmentViewModel appointment);
    }
}
