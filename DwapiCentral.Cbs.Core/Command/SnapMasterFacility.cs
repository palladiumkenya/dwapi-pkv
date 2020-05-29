using System;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class SnapMasterFacility:IRequest<bool>
    {
        public int SiteCode { get; }

        public SnapMasterFacility(int siteCode)
        {
            SiteCode = siteCode;
        }
    }

}
