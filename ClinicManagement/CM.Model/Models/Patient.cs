using CM.Tools.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Guid CityFk { get; set; }
        public Guid DoctorFk { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public Guid MedicineFk { get; set; }
        [ForeignKey("MedicineFk")]
        public Medicine Medicine { get; set; }
        [ForeignKey("CityFk")]
        public City Cities { get; set; }
        [ForeignKey("DoctorFk")]
        public Doctor Doctor { get; set; }
    }
}
