using CM.Tools.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Model.Models
{
    public class Person : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string DOA { get; set; }
        [Required]
        public string Phone { get; set; }

        public string MailId { get; set; }
    }
}
