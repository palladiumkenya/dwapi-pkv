using System;
using System.ComponentModel.DataAnnotations.Schema;
using DwapiCentral.SharedKernel.Enums;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Model
{
    public class Cargo : Entity<Guid>
    {
        public CargoType Type { get; set; }
        public string Items { get; set; }
        public Guid ManifestId { get; set; }

        public Cargo()
        {
        }
    }
}