using SLACalculator.Application.Common.Interfaces;
using SLACalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SLACalculator.Domain.Entities;

namespace SLACalculator.Infrastructure.Services
{
    public class SlaCalculatorService : ISlaCalculatorService
    {
        private readonly IApplicationDbContext _context;

        public SlaCalculatorService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DateTime> CalculateSlaExpirationAsync(Priority priority, DateTime captureDateTime, CancellationToken cancellationToken = default)
        {
            var slaConfig = await _context.SlaConfigurations
                .FirstOrDefaultAsync(s => s.Priority == priority, cancellationToken);

            if (slaConfig == null)
                throw new Exception($"SLA configuration not found for priority: {priority}");

            int slaHours = slaConfig.ResolutionTimeInHours;

            var workingHours = await _context.WorkingHours.FirstOrDefaultAsync(cancellationToken);
            if (workingHours == null)
                throw new Exception("Working hours configuration not found");

            var workingDays = await _context.WorkingDays
                                            .Where(w => w.IsWorkingDay)
                                            .Select(w => w.DayOfWeek)
                                            .ToListAsync(cancellationToken);

            var businessClosures = await _context.BusinessClosures
                                                 .Where(c => c.ClosureDate >= captureDateTime.Date)
                                                 .ToListAsync(cancellationToken);

            //1- Add SLA time to capture time assumog no closures
            DateTime slaExpiration = captureDateTime.AddHours(slaHours);

            //2- Start from the next hour of capture time
            DateTime currentHour = captureDateTime.AddHours(1);
            currentHour = new DateTime(currentHour.Year, currentHour.Month, currentHour.Day, currentHour.Hour, 0, 0);

            int businessHoursNeeded = slaHours;
            int businessHoursCounted = 0;

            //3- Loop through next hours till count enough business hours
            while (businessHoursCounted < businessHoursNeeded)
            {
                bool isBusinessHour = IsBusinessHour(currentHour, workingDays, workingHours, businessClosures);

                if (isBusinessHour)
                    businessHoursCounted++;
                else
                    slaExpiration = slaExpiration.AddHours(1);

                currentHour = currentHour.AddHours(1);
            }

            return slaExpiration;
        }

        private bool IsBusinessHour(DateTime dateTime, List<DayOfWeek> workingDays, WorkingHours workingHours, List<BusinessClosure> businessClosures)
        {
            if (!workingDays.Contains(dateTime.DayOfWeek))
                return false;

            TimeSpan currentTime = dateTime.TimeOfDay;
            if (currentTime < workingHours.StartTime || currentTime >= workingHours.EndTime)
                return false;

            var closure = businessClosures.FirstOrDefault(c => c.ClosureDate.Date == dateTime.Date);
            if (closure != null)
            {
                if (closure.IsFullDayClosure || (closure.StartTime.HasValue && closure.EndTime.HasValue && currentTime >= closure.StartTime.Value && currentTime < closure.EndTime.Value))
                    return false;
            }

            return true;
        }
    }
}
