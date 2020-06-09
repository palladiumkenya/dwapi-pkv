using System;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class EnrollFacility : IRequest<Guid>
    {
        public int SiteCode { get; }
        public string Name { get;  }
        public string Emr { get; set; }
        public bool IsMgs { get; }
        public bool AllowSnapshot { get; set; }

        public EnrollFacility(int siteCode, string name,string emr,  bool isMgs=false)
        {
            SiteCode = siteCode;
            Name = name;
            Emr = emr;
            IsMgs = isMgs;
        }
    }
}
