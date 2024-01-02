using Dapper;
using Repository.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        
        public async Task ExecCommand(string SpName, object Params)
        {
            using(IDbConnection con = new SqlConnection(CommonConstants.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                await con.QueryAsync<T>(SpName, Params, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T> FindExecCommand(string SpName, object Params)
        {
            using (IDbConnection con = new SqlConnection(CommonConstants.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var data = await con.QuerySingleOrDefaultAsync<T>(SpName, Params, commandType: CommandType.StoredProcedure);
                return data;
            }
        }
        public async Task<IList<T>> ListData(string SpName, object Params)
        {
            using (IDbConnection con = new SqlConnection(CommonConstants.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                var data = await con.QueryAsync<T>(SpName, Params, commandType: CommandType.StoredProcedure);
                return data.ToList();
            }
        }
    }
}
