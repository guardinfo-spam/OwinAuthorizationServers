using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AuthZ.DataLayer;
using System.Configuration;

namespace AuthZ.Datalayer
{
    public class DataLayerDapper : IDataLayer
    {
        IDbConnection _connection;
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["AuthZ.ConnectionString"].ConnectionString;

        public IDbConnection Connection
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._connectionString))
                {
                    throw new Exception("connection string missing");
                }

                this._connection = new SqlConnection(_connectionString);
                return this._connection;
            }
        }

        public AudienceDto GetAudience(string clientId)
        {
            using (IDbConnection conn = this.Connection)
            {
                return conn.Query<AudienceDto>("SELECT * FROM Audience WHERE ClientID = @clientID", new { clientID = clientId }).FirstOrDefault();
            }
        }

        public IEnumerable<AudienceDto> GetAll()
        {
            using (var conn = this.Connection)
            {
                return conn.Query<AudienceDto>("SELECT * FROM Audience").ToList();
            }
        }
    }
}