using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Enums;
using MediatR;

namespace DwapiCentral.Cbs.Core.CommandHandler
{
    public class ProcessManifestHandler : IRequestHandler<ProcessManifest,bool>
    {
        private readonly IManifestRepository _repository;

        public ProcessManifestHandler(IManifestRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ProcessManifest request, CancellationToken cancellationToken)
        {

            var manifests = _repository.GetAll(x => x.Status == ManifestStatus.Staged).ToList();
            if(manifests.Any())
                await  _repository.ClearFacility(manifests);

            return true;
        }
    }
}