using System;

namespace DwapiCentral.SharedKernel.Exceptions
{
    public class SubscriberNotAuthorizedException:Exception
    {
        public SubscriberNotAuthorizedException(string name):base($"Subscriber {name} not authorized")
        {
            
        }
    }
}