using MediatR;
using Microsoft.EntityFrameworkCore;
using SLACalculator.Application.Common.Interfaces;
using SLACalculator.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Application.Features.SLA.Queries
{
    internal class CalculateSlaQueryHandler : IRequestHandler<CalculateSlaQuery, CalculateSlaResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISlaCalculatorService _slaCalculator;

        public CalculateSlaQueryHandler(
            IApplicationDbContext context,
            ISlaCalculatorService slaCalculator)
        {
            _context = context;
            _slaCalculator = slaCalculator;
        }

        public async Task<CalculateSlaResponse> Handle(CalculateSlaQuery request,CancellationToken cancellationToken)
        {
            var slaConfig = await _context.SlaConfigurations
                .FirstOrDefaultAsync(s => s.Priority == request.Priority, cancellationToken);

            if (slaConfig == null)
                throw new Exception($"SLA configuration not found for priority: {request.Priority}");

            var slaExpiration = await _slaCalculator.CalculateSlaExpirationAsync(
                request.Priority,
                request.CaptureDateTime,
                cancellationToken);

            return new CalculateSlaResponse
            {
                Priority = request.Priority,
                CaptureDateTime = request.CaptureDateTime,
                SlaExpirationDateTime = slaExpiration,
                SlaTimeInHours = slaConfig.ResolutionTimeInHours
            };
        }
    }
}
