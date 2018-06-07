using System;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class EnrollFacility : IRequest<Guid>
    {
        public int SiteCode { get; }
        public string Name { get;  }

        public EnrollFacility(int siteCode, string name)
        {
            SiteCode = siteCode;
            Name = name;
        }
    }
}