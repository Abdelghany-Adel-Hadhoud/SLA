using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Domain.Entities
{
    public class WorkingDay
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public bool IsWorkingDay { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
