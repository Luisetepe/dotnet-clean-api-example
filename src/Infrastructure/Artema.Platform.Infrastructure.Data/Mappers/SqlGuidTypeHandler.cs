using System.Data;
using Dapper;

namespace Artema.Platform.Infrastructure.Data.Mappers;

public class SqlGuidTypeHandler : SqlMapper.TypeHandler<Guid?>
{
    public override void SetValue(IDbDataParameter parameter, Guid? guid)
    {
        parameter.Value = guid;
    }

    public override Guid? Parse(object? value)
    {
        if (value is Guid guid)
            return Guid.Parse(guid.ToString());

        return value is null ? null : new Guid(value.ToString()!);
    }
}