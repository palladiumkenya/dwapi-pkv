using System;
using Dapper;

namespace DwapiCentral.Cbs.Core.Tests
{
    public class StringGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            return new Guid(value.ToString());
        }

        public override void SetValue(System.Data.IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString();
        }
    }
}