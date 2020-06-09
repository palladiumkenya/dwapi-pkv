﻿using System;
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

        public Cargo(CargoType type, string items, Guid manifestId)
        {
            Type = type;
            Items = items;
            ManifestId = manifestId;
        }
    }
}
