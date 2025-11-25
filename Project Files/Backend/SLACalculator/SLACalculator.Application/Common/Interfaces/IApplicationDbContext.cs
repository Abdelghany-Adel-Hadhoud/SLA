using Microsoft.EntityFrameworkCore;
using SLACalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalculator.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<WorkingDay> WorkingDays { get; }
        DbSet<WorkingHours> WorkingHours { get; }
        DbSet<BusinessClosure> BusinessClosures { get; }
        DbSet<SlaConfiguration> SlaConfigurations { get; }
        DbSet<Complaint> Complaints { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
