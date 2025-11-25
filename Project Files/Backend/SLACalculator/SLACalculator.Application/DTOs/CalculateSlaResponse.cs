using SLACalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Application.DTOs
{
    public class CalculateSlaResponse
    {
        public Priority Priority { get; set; }
        public DateTime CaptureDateTime { get; set; }
        public DateTime SlaExpirationDateTime { get; set; }
        public int SlaTimeInHours { get; set; }
    }
}
