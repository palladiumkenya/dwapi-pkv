using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DwapiCentral.Cbs.Core.Command;
using DwapiCentral.Cbs.Core.Interfaces;
using DwapiCentral.Cbs.Core.Interfaces.Repository;
using DwapiCentral.SharedKernel.Exceptions;
using DwapiCentral.SharedKernel.Utils;
using MediatR;

namespace DwapiCentral.Cbs.Core.CommandHandler
{
    public class VerifySubscriberHandler : IRequestHandler<VerifySubscriber, string>
    {
        private readonly IDocketRepository _repository;

        public VerifySubscriberHandler(IDocketRepository repository)
        {
            _repository = repository;
        }


        public async Task<string> Handle(VerifySubscriber request, CancellationToken cancellationToken)
        {
            var docket = await _repository.GetAsync(request.Docket);

            if (null == docket)
                throw new DocketNotFoundException(request.Docket);

            if (!docket.SubscriberExists(request.Name))
                throw new SubscriberNotFoundException(request.Name);

            if (docket.SubscriberAuthorized(request.Name, request.AuthCode))
                    return docket.Name;

            throw new SubscriberNotAuthorizedException(request.Name);
        }
    }
}