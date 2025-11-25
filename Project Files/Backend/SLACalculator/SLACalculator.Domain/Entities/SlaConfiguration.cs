using SLACalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Domain.Entities
{
    //SLA time configuration for each priority level
    public class SlaConfiguration
    {
        public int Id { get; set; }
        public Priority Priority { get; set; }
        public int ResolutionTimeInHours { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
