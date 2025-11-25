using SLACalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Domain.Entities
{
    public class Complaint
    {
        public int Id { get; set; }
        public Priority Priority { get; set; }
        public DateTime CaptureDateTime { get; set; }
        public DateTime SlaExpirationDateTime { get; set; }
        public string? FilePaths { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
    }
}
