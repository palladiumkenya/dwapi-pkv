﻿using System;
using System.Threading;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using MediatR;

namespace DwapiCentral.Cbs.Core.CommandHandler
{
    public class SaveManifestHandler : IRequestHandler<SaveManifest, Guid>
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _repository;

        public SaveManifestHandler(IMediator mediator, IManifestRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Guid> Handle(SaveManifest request, CancellationToken cancellationToken)
        {
            var facilityId = await _mediator.Send(new EnrollFacility(request.Manifest.SiteCode,request.Manifest.Name,request.Manifest.EmrName), cancellationToken);

            request.Manifest.UpdateFacility(facilityId);
            _repository.Create(request.Manifest);
            await _repository.SaveAsync();

            return request.Manifest.Id;
        }
    }
}
