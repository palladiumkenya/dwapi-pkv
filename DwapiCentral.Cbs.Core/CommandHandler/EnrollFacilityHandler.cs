using System;
using System.Threading;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using MediatR;

namespace DwapiCentral.Cbs.Core.CommandHandler
{
    public class EnrollFacilityHandler: IRequestHandler<EnrollFacility,Guid>
    {
        private readonly IMasterFacilityRepository _masterFacilityRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IMediator _mediator;

        public EnrollFacilityHandler(IMasterFacilityRepository masterFacilityRepository, IFacilityRepository facilityRepository, IMediator mediator)
        {
            _masterFacilityRepository = masterFacilityRepository;
            _facilityRepository = facilityRepository;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(EnrollFacility request, CancellationToken cancellationToken)
        {
            var mfl =await  _mediator.Send(new ValidateFacility(request.SiteCode), cancellationToken);

            var facility =await _facilityRepository.GetAsync(x => x.SiteCode == request.SiteCode);

            if (null == facility)
            {
                var newFacility = new Facility(request.SiteCode, request.Name, mfl.Id);
                _facilityRepository.Create(newFacility);
                await _facilityRepository.SaveAsync();
                return newFacility.Id;
            }

            return facility.Id;
        }
    }
}