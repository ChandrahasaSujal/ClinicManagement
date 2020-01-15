using AutoMapper;
using CM.Data.Infrastructure;
using CM.Data.ViewModels.Appointment;
using CM.Model.Models;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CM.Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;

        private Person Person { get; set; }
        private AppointmentViewModel Appointment { get; set; }
        private List<Person> People { get; set; }
        private List<AppointmentViewModel> Appointments { get; set; }

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper,IEmailService emailService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.emailService = emailService;
        }
        public List<AppointmentViewModel> GetAppointments()
        {
            try
            {
                People = new List<Person>();
                Appointments = new List<AppointmentViewModel>();
                People = unitOfWork.PeopleRepository.Fetch(p => p.IsDeleted == false).ToList();
                if (People != null)
                {
                    Appointments = mapper.Map(People, Appointments);
                    return Appointments.OrderByDescending(a => a.CreatedDate).ToList();
                }
                return null;
            }
            catch (Exception ex)
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
                    Person = mapper.Map(appointment, Person);
                    Person.Id = Guid.NewGuid();
                    unitOfWork.PeopleRepository.Add(Person);
                    unitOfWork.SaveChanges();

                    //send appointment as an email to the owner

                    emailService.SendAppointmentEMail(appointment);

                    return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool UpdateAppointment(AppointmentViewModel appointment)
        {
            try
            {
                if (appointment != null)
                {
                    Person = new Person();
                    Person = unitOfWork.PeopleRepository.FirstOrDefault(p => p.Id == appointment.Id);
                    if (Person != null)
                    {
                        Person = mapper.Map(appointment, Person);
                        unitOfWork.PeopleRepository.Update(Person);
                        unitOfWork.SaveChanges();
                        return true;
                    }
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
                        Person = unitOfWork.PeopleRepository.FirstOrDefault(p => p.Id == appointmentGuid);
                        Appointment = mapper.Map(Person, Appointment);
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

        public bool DeleteAppointment(Guid appointmentId)
        {
            try
            {
                if (appointmentId != null)
                {
                    unitOfWork.PeopleRepository.LogicalDelete(appointmentId);
                    unitOfWork.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        public AppointmentViewModel GetCustomerByPhoneNumber(string phoneNumber)
        {
            try
            {
                Appointment = new AppointmentViewModel();
                Person = new Person();
                Person = unitOfWork.PeopleRepository.FirstOrDefault(p=>p.Phone == phoneNumber && p.IsDeleted==false);
                if (Person != null)
                {
                    Appointment = mapper.Map(Person, Appointment);
                    return Appointment;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
