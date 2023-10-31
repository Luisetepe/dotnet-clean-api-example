using Artema.Platform.Application.Interfaces;
using RT.Comb;

namespace Artema.Platform.Infrastructure.Common.Services;

public class PgUniqueIdentifierGenerator : IUniqueIdentifierGenerator
{
    public Guid Generate()
    {
        return Provider.PostgreSql.Create();
    }
}