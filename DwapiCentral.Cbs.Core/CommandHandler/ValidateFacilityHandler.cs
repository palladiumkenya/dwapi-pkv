using System;
using System.Threading;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Exceptions;
using MediatR;

namespace DwapiCentral.Cbs.Core.CommandHandler
{
    public class ValidateFacilityHandler: IRequestHandler<ValidateFacility,MasterFacility>
    {
        private readonly IMasterFacilityRepository _repository;

        public ValidateFacilityHandler(IMasterFacilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<MasterFacility> Handle(ValidateFacility request, CancellationToken cancellationToken)
        {
            var masterFacility =await _repository.GetAsync(request.SiteCode);

            if (null==masterFacility)
                throw new FacilityNotFoundException(request.SiteCode);

            return masterFacility;
        }
    }
}