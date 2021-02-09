using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.Cbs.Core.Model.Dto;

namespace DwapiCentral.Cbs.Core.Interfaces.Service
{
    public interface ILiveSyncService
    {
        void SyncManifest(Manifest manifest,int clientCount,string docket);
        void SyncStats(List<Guid> facilityId);
       void SyncMetrics(List<MetricDto> metrics);
       Task SyncHandshake(List<HandshakeDto> handshakeDtos);
    }
}
