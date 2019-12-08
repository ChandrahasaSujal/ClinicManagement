using CM.Data.Infrastructure;
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
        IEnumerable<Patient> patients;
        public AppointmentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public IEnumerable<Patient> GetAppointments()
        {
            patients = new List<Patient>();
            patients = _unitOfWork.PatientRepository.Fetch();
            return patients;
        }
    }
}
