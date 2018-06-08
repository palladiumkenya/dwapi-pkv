using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.SharedKernel.Exceptions;
using MediatR;

namespace DwapiCentral.Cbs.Core.CommandHandler
{
    public class SaveMpiHandler : IRequestHandler<SaveMpi, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IFacilityRepository _repository;
        private readonly IMasterPatientIndexRepository _indexRepository;
        public SaveMpiHandler(IMediator mediator, IFacilityRepository repository, IMasterPatientIndexRepository indexRepository)
        {
            _mediator = mediator;
            _repository = repository;
            _indexRepository = indexRepository;
        }

        public async Task<Guid> Handle(SaveMpi request, CancellationToken cancellationToken)
        {
            var facility = await _repository.GetAsync(x => x.Id == request.FacilityId);

            if (null==facility)
                throw new FacilityNotFoundException(request.FacilityId);


            if (request.MasterPatientIndices.Any())
            {
                var patients = request.MasterPatientIndices.ToList();
                patients.ForEach(x => x.FacilityId = facility.Id);
                _indexRepository.CreateBulk(patients);
            }

            return request.FacilityId;
        }
    }
}