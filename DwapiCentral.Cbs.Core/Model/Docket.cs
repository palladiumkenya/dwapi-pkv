using DwapiCentral.SharedKernel.Model;

namespace DwapiCentral.Cbs.Core.Model
{
    public class Docket:Entity<string>
    {
        public string Name { get; set; }
        public string Instance { get; set; }
    }
}