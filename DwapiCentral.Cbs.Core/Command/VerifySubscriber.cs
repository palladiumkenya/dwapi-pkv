using MediatR;

namespace DwapiCentral.Cbs.Core.Command
{
    public class VerifySubscriber : IRequest<string>
    {
        public string Docket { get; set; }
        public string Name { get; }
        public string AuthCode { get; }

        public VerifySubscriber(string docket, string name, string authCode)
        {
            Docket = docket;
            Name = name;
            AuthCode = authCode;
        }
    }
}