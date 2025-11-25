using SLACalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Application.DTOs
{
    public class CalculateSlaRequest
    {
        public Priority Priority { get; set; }
        public DateTime CaptureDateTime { get; set; }
        public List<string>? FileNames { get; set; }
    }
}
