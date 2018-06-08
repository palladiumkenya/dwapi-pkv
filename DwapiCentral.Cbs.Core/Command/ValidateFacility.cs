using DwapiCentral.Cbs.Core.Model;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class ValidateFacility: IRequest<MasterFacility>
    {
        public int SiteCode { get; }

        public ValidateFacility(int siteCode)
        {
            SiteCode = siteCode;
        }
    }
}