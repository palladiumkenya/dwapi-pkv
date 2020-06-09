using System;
using DwapiCentral.Cbs.Core.Model;
using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class SaveManifest : IRequest<Guid>
    {
        public Manifest Manifest { get; set; }

        public bool IsMgs { get; set; }

        public SaveManifest()
        {
        }

        public SaveManifest(Manifest manifest)
        {
            Manifest = manifest;
        }
    }
}
