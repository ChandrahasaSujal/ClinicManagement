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
        private Person Person { get; set; }
        private AppointmentViewModel Appointment { get; set; }
        private IEnumerable<Person> People { get; set; }
        private IEnumerable<AppointmentViewModel> Appointments { get; set; }

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<AppointmentViewModel> GetAppointments()
        {
            try
            {
                People = new List<Person>();
                Appointments = new List<AppointmentViewModel>();
                People = _unitOfWork.PeopleRepository.Fetch();
                if (People.Any())
                {
                    Appointments = _mapper.Map(People, Appointments);
                    return Appointments;
                }
                return null;
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
                if (appointment != null)
                {
                    Person = new Person();
                    Person = _mapper.Map(appointment, Person);
                    Person.Id = Guid.NewGuid();
                    _unitOfWork.PeopleRepository.Add(Person);
                    _unitOfWork.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool EditAppointment(AppointmentViewModel appointment)
        {
            try
            {
                if (appointment != null)
                {
                    Person = new Person();
                    Person = _unitOfWork.PeopleRepository.FirstOrDefault(p => p.Id == appointment.Id);
                    Person = _mapper.Map(appointment, Person);
                    _unitOfWork.PeopleRepository.Update(Person);
                    _unitOfWork.SaveChanges();
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public AppointmentViewModel GetAppointment(string appointmentId)
        {
            try
            {
                if (!string.IsNullOrEmpty(appointmentId))
                {
                    if (true)
                    {
                        Guid appointmentGuid;
                        Guid.TryParse(appointmentId, out appointmentGuid);
                        Person = new Person();
                        Appointment = new AppointmentViewModel();
                        Person = _unitOfWork.PeopleRepository.FirstOrDefault(p => p.Id == appointmentGuid);
                        Appointment = _mapper.Map(Person, Appointment);
                        return Appointment;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
    }
}
