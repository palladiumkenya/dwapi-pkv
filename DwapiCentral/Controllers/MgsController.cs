using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.Cbs.Core.Interfaces.Service;
using DwapiCentral.SharedKernel.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MgsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMgsManifestService _manifestService;
        private readonly IMgsService _mgsService;
        private readonly IMetricMigrationExtractRepository _repository;

        public MgsController(IMediator mediator, IManifestRepository manifestRepository, IMetricMigrationExtractRepository repository, IMgsManifestService manifestService, IMgsService mgsService)
        {
            _mediator = mediator;
            _repository = repository;
            _manifestService = manifestService;
            _mgsService = mgsService;
        }

        // POST api/mgs/verify
        [HttpPost("Verify")]
        public async Task<IActionResult> Verify([FromBody] VerifySubscriber subscriber)
        {
            if (null == subscriber)
                return BadRequest();

            try
            {
                var dockect = await _mediator.Send(subscriber, HttpContext.RequestAborted);
                return Ok(dockect);
            }
            catch (Exception e)
            {
                Log.Error(e, "verify error");
                return StatusCode(500, e.Message);
            }
        }

        // POST api/mgs/Manifest
        [HttpPost("Manifest")]
        public async Task<IActionResult> ProcessManifest([FromBody] SaveManifest manifest)
        {
            if (null == manifest)
                return BadRequest();

            try
            {
                var faciliyKey = await _mediator.Send(manifest, HttpContext.RequestAborted);
                BackgroundJob.Enqueue(() => _manifestService.Process());
                return Ok(new
                {
                    FacilityKey = faciliyKey
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }

        // POST api/mgs/Mpi
        [HttpPost("migrations")]
        public IActionResult ProcessMpi([FromBody] SaveMgs mpi)
        {
            if (null == mpi)
                return BadRequest();

            try
            {
                var id=  BackgroundJob.Enqueue(() => _mgsService.Process(mpi.Migrations));
                return Ok(new
                {
                    BatchKey = id
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }
    }
}
