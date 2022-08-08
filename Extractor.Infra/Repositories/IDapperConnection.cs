using System.Data;

namespace Extractor.Infra.Repositories
{
    public interface IDapperConnection : IDisposable
    {
        IDbConnection GetConnection();
    }
}
