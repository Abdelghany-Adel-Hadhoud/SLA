using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Domain.Entities
{
    public class BusinessClosure
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ClosureDate { get; set; }
        //if not full day closure,will use StartTime and EndTime
        public bool IsFullDayClosure { get; set; } = true;
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
