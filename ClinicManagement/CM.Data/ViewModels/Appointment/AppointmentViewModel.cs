using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Data.ViewModels.Appointment
{
    public class AppointmentViewModel : BaseViewModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public string Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public int Age { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please Provide Valid Phone Numbers")]
        public string PhoneNumber { get; set; }

        [Display(Name ="Email-ID")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Please Provide Valid Email")]
        public string EMail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This Field is requiered")]
        public string DOA { get; set; }
    }
}
