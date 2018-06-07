using System;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Interfaces;

namespace DwapiCentral.Cbs.Core.Interfaces
{
    public interface IFacilityRepository : IRepository<Facility, Guid>
    {
        bool IsEnrolled(int siteCode);
    }
}