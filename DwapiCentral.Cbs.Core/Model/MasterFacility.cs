using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using DwapiCentral.SharedKernel;
using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Model
{
    public class MasterFacility:Entity<int>
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public ICollection<Facility> Mentions { get; set; }=new List<Facility>();
    }
}