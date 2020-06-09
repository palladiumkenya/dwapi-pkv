using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.SharedKernel.DTOs;
using MediatR;

namespace DwapiCentral.Cbs.Core.CommandHandler
{
    public class SearchMpiHandler : IRequestHandler<SearchMpi, List<MpiSearchResultDto>>
    {
        private readonly IMasterPatientIndexRepository _masterPatientIndexRepository;

        public SearchMpiHandler(IMasterPatientIndexRepository masterPatientIndexRepository)
        {
            _masterPatientIndexRepository = masterPatientIndexRepository;
        }

        public async Task<List<MpiSearchResultDto>> Handle(SearchMpi request, CancellationToken cancellationToken)
        {
            var response = await Task.Run(()=>_masterPatientIndexRepository.MpiSearch(request), cancellationToken);
            return response;
        }
    }
}