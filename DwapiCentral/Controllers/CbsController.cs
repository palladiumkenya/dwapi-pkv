using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CbsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;
        private readonly IMasterPatientIndexRepository _masterPatientIndexRepository;

        public CbsController(IMediator mediator, IManifestRepository manifestRepository, IMasterPatientIndexRepository masterPatientIndexRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
            _masterPatientIndexRepository = masterPatientIndexRepository;
        }

        // POST api/cbs/verify
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

        // POST api/cbs/Manifest
        [HttpPost("Manifest")]
        public async Task<IActionResult> Manifest([FromBody] SaveManifest manifest)
        {
            if (null == manifest)
                return BadRequest();

            try
            {
                var faciliyKey = await _mediator.Send(manifest, HttpContext.RequestAborted);
                BackgroundJob.Enqueue(() => _manifestRepository.Process());
                return Ok(faciliyKey);
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }

        // POST api/cbs/Manifest
        [HttpPost("Mpi")]
        public async Task<IActionResult> Mpi([FromBody] SaveMpi mpi)
        {
            if (null == mpi)
                return BadRequest();

            try
            {
                var faciliyKey = await _mediator.Send(new ValidateFacilityKey(mpi.FacilityId), HttpContext.RequestAborted);
                BackgroundJob.Enqueue(() => _masterPatientIndexRepository.Process(mpi.FacilityId,mpi.MasterPatientIndices));
                return Ok(faciliyKey);
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }
    }
}
