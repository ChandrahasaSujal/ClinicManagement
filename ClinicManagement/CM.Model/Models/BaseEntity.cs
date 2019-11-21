using System;

namespace CM.Model.Models
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public BaseEntity()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}