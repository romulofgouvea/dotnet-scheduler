using Extractor.Domain.Models;
using Extractor.Infra.Repositories;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Extractor.Infra
{
    public class DapperConnection : IDapperConnection
    {
        private SqlConnection _db;
        private AppSettingsConfiguration _config;

        public DapperConnection(IOptions<AppSettingsConfiguration> config)
        {
            _config = config.Value;
        }

        public void Dispose()
        {
            if (_db != null && _db.State != ConnectionState.Closed)
            {
                _db.Close();
                _db.Dispose();
            }
        }

        public IDbConnection GetConnection() => _db = new SqlConnection(_config.ConnectionString);
    }
}
