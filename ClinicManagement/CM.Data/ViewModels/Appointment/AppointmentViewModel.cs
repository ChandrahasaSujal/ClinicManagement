using CM.Tools.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.ViewModels.Appointment
{
    public class AppointmentViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "This Name is requiered")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Gender is requiered")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "This Phone is requiered")]
        public string Phone { get; set; }
        public string MailId { get; set; }
        [Required(ErrorMessage = "This Date of Appointment is requiered")]
        public DateTime DOA { get; set; }
    }
}
