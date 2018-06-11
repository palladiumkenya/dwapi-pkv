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
    public class ValidateFacilityKeyHandler: IRequestHandler<ValidateFacilityKey,bool>
    {
        private readonly IFacilityRepository _repository;

        public ValidateFacilityKeyHandler(IFacilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ValidateFacilityKey request, CancellationToken cancellationToken)
        {
            var masterFacility =await _repository.GetAsync(x=>x.Id==request.Key);

            if (null==masterFacility)
                throw new FacilityNotFoundException(request.Key);

            return true;
        }
    }
}