using System;
using System.Collections.Generic;
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
    public class CbsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IManifestService _manifestService;
        private readonly IMpiService _mpiService;
        private readonly IMasterPatientIndexRepository _masterPatientIndexRepository;

        public CbsController(IMediator mediator, IManifestRepository manifestRepository, IMasterPatientIndexRepository masterPatientIndexRepository, IManifestService manifestService, IMpiService mpiService)
        {
            _mediator = mediator;
            _masterPatientIndexRepository = masterPatientIndexRepository;
            _manifestService = manifestService;
            _mpiService = mpiService;
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
        public async Task<IActionResult> ProcessManifest([FromBody] SaveManifest manifest)
        {
            if (null == manifest)
                return BadRequest();

            try
            {
                manifest.IsMgs = false;
                var faciliyKey = await _mediator.Send(manifest, HttpContext.RequestAborted);
                BackgroundJob.Enqueue(() => _manifestService.Process(true));
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

        // POST api/cbs/Mpi
        [HttpPost("Mpi")]
        public IActionResult ProcessMpi([FromBody] SaveMpi mpi)
        {
            if (null == mpi)
                return BadRequest();

            try
            {
                var id=  BackgroundJob.Enqueue(() => _mpiService.Process(mpi.MasterPatientIndices,true));
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

        // POST api/cbs/Manifest
        [HttpPost("mpiSearch")]
        public async Task<IActionResult> SearchMpi([FromBody] SearchMpi search)
        {
            if (null == search)
                return BadRequest();
            try
            {
                var results = await _mediator.Send<List<MpiSearchResultDto>>(search, HttpContext.RequestAborted);
                return Ok(results);
            }
            catch (Exception e)
            {
                Log.Error(e, "search error");
                return StatusCode(500, e.Message);
            }
        }
    }
}
