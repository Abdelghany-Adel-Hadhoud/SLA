using MediatR;
using SLACalculator.Application.DTOs;
using SLACalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Application.Features.SLA.Queries
{
    public class CalculateSlaQuery : IRequest<CalculateSlaResponse>
    {
        public Priority Priority { get; set; }
        public DateTime CaptureDateTime { get; set; }
    }
}
