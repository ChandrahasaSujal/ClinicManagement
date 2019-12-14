using AutoMapper;
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
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<AppointmentViewModel> GetAppointments()
        {
            try
            {
                IEnumerable<Person> people = new List<Person>();
                IEnumerable<AppointmentViewModel> appointments = new List<AppointmentViewModel>();
                people = _unitOfWork.PersonRepository.Fetch();
                _mapper.Map(people, appointments);
                return appointments;
            }
            catch (Exception)
            {

            }
            return null;
        }

        public bool AddAppointment(AppointmentViewModel appointment)
        {
            try
            {
                Person person = new Person();
                person = _mapper.Map(appointment, person);
                person.Id = Guid.NewGuid();
                _unitOfWork.PersonRepository.Add(person);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }
    }
}
