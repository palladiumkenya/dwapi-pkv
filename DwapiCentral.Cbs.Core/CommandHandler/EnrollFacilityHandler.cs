using System;
using System.Threading;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces;
using MediatR;

namespace DwapiCentral.Cbs.Core.CommandHandler
{
    public class EnrollFacilityHandler: IRequestHandler<EnrollFacility,Guid>
    {
        private readonly IMasterFacilityRepository _masterFacilityRepository;
        private readonly IFacilityRepository _facilityRepository;

        public EnrollFacilityHandler(IMasterFacilityRepository masterFacilityRepository, IFacilityRepository facilityRepository)
        {
            _masterFacilityRepository = masterFacilityRepository;
            _facilityRepository = facilityRepository;
        }

        public Task<Guid> Handle(EnrollFacility request, CancellationToken cancellationToken)
        {
            var facility = _facilityRepository.Get(x => x.SiteCode == request.SiteCode);
            if (null != facility)
                return Task.FromResult(facility.Id);


            throw new NotImplementedException();
        }
    }
}