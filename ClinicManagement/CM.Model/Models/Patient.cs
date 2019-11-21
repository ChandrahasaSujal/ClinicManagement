using CM.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models
{
    public class Patient : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string MailId { get; set; }
        public string Token { get; set; }
        public DateTime BirthDate { get; set; }
        public byte CityId { get; set; }
        public City Cities { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
    }
}
