using System;
using System.Threading;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.SharedKernel.Exceptions;
using MediatR;

namespace DwapiCentral.Cbs.Core.CommandHandler
{
    public class ValidateFacilityHandler: IRequestHandler<ValidateFacility,string>
    {
        private readonly IMasterFacilityRepository _repository;

        public ValidateFacilityHandler(IMasterFacilityRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(ValidateFacility request, CancellationToken cancellationToken)
        {
            var facility =await _repository.GetAsync(request.SiteCode);

            if (null==facility)
                throw new FacilityNotFoundException(request.SiteCode);

            return $"{facility}";
        }
    }
}