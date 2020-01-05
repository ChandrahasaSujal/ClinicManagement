using CM.Data.ViewModels.Appointment;
using System;
using System.Collections.Generic;


namespace CM.Service.ServiceInterfaces
{
    public interface IAppointmentService
    {
        List<AppointmentViewModel> GetAppointments();
        AppointmentViewModel GetAppointment(string appointmentId);
        bool AddAppointment(AppointmentViewModel appointment);
        bool UpdateAppointment(AppointmentViewModel appointment);
        bool DeleteAppointment(Guid appointmentId);
        AppointmentViewModel GetCustomerByPhoneNumber(string phoneNumber);
    }
}
