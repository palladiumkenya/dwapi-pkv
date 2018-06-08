using System;

namespace DwapiCentral.SharedKernel.Exceptions
{
    public class SubscriberNotFoundException : Exception
    {
        public SubscriberNotFoundException(string name) : base($"Subscriber {name} not found")
        {

        }
    }
}