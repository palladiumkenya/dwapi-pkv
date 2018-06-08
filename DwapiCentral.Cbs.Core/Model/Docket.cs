using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DwapiCentral.SharedKernel.Model;
using DwapiCentral.SharedKernel.Utils;

namespace DwapiCentral.Cbs.Core.Model
{
    public class Docket:Entity<string>
    {
        public string Name { get; set; }
        public string Instance { get; set; }
        public ICollection<Subscriber> Subscribers { get; set; }

        public Docket()
        {
        }
        public bool SubscriberExists(string name)
        {
            return Subscribers.Any(x => x.Name.IsSameAs(name));
        }

        public bool SubscriberAuthorized(string name, string authcode)
        {
            return Subscribers.Any(x => x.Name.IsSameAs(name) && x.AuthCode.IsSameAs(authcode));
        }
    }
}