using SLACalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Application.Common.Interfaces
{
    public interface ISlaCalculatorService
    {
        Task<DateTime> CalculateSlaExpirationAsync(Priority priority, DateTime captureDateTime, CancellationToken cancellationToken = default);
    }
}
