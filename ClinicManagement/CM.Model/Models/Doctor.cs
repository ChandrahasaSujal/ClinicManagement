using System;

namespace CM.Model.Models
{
    public class Doctor : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsAvailable { get; set; }
        public string Address { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}