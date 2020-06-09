using System;
using System.Linq;
using DwapiCentral.Cbs.Core.Model;
using DwapiCentral.SharedKernel.Enums;
using Newtonsoft.Json;

namespace DwapiCentral.Cbs.Core.Exchange
{
    public class ManifestDto
    {
        public Guid Id { get; set; }
        public int FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string Docket { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime BuildDate { get; set; }
        public int PatientCount { get; set; }
        public string Cargo { get; set; }

        public ManifestDto(Manifest manifest, int count,string docket)
        {
            Id = manifest.Id;
            FacilityCode = manifest.SiteCode;
            FacilityName = manifest.Name;
            Docket = docket;
            LogDate = manifest.DateLogged;
            BuildDate = manifest.DateArrived;
            PatientCount = count;
            var cargoes = manifest.Cargoes.Where(x => x.Type != CargoType.Patient).ToList();
            if(cargoes.Any())
                Cargo = JsonConvert.SerializeObject(cargoes);
        }
    }
}
