using System;
using DwapiCentral.Cbs.Core.Model;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class SaveManifest : IRequest<Guid>
    {
        public int SiteCode { get;  }
        public string Name { get; }
        public Manifest Manifest { get; }

        public SaveManifest(int siteCode, string name, Manifest manifest)
        {
            SiteCode = siteCode;
            Name = name;
            Manifest = manifest;
        }
    }
}