using System;

namespace CM.Model.Models
{
    public class City : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}