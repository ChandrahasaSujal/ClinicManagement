using CM.Tools.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.ViewModels.Appointment
{
    public class AppointmentViewModel
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public Gender Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please Provide Valid Phone Numbers")]
        public string Phone { get; set; }

        [Display(Name ="Email-ID")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        public string MailId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public string DOA { get; set; }
    }
}
