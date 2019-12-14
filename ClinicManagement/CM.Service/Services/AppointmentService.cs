using CM.Data.Infrastructure;
using CM.Data.ViewModels.Appointment;
using CM.Model.Models;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<AppointmentViewModel> GetAppointments()
        {
            IEnumerable<Patient> patients;
            IEnumerable<AppointmentViewModel> appointments;
            appointments = new List<AppointmentViewModel>();
            patients = new List<Patient>();
            patients = _unitOfWork.PatientRepository.Fetch();
            foreach (var item in patients)
            {
                var appointment = new AppointmentViewModel();
                appointment.Id = item.Id;
                appointment.Name = item.Name;
                appointment.DOA = item.DOA;
                appointment.Gender = item.Gender;
                appointment.Phone = item.Phone;

            }
            return appointments;
        }

        public bool AddAppointment(AppointmentViewModel appointment)
        {
            if (appointment != null)
            {
                Patient patient = new Patient();
                patient.Id = appointment.Id;
                patient.Name = appointment.Name;
                patient.Gender = appointment.Gender;
                patient.Phone = appointment.Phone;
                patient.MailId = appointment.MailId;
                var item =_unitOfWork.PatientRepository.Add(patient);
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
