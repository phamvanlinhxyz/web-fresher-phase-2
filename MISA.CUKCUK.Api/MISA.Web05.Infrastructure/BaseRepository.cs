using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MySqlConnector;

namespace MISA.Web05.Infrastructure
{
    /// <summary>
    /// Lớp repository cha
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: linhpv (08/08/2022)
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region Variable
        protected string ConnectionString;
        protected MySqlConnection SqlConnection;
        protected string TableName;
        #endregion

        #region Contructor
        public BaseRepository(IConfiguration configuration)
        {
            // Khai báo kết nối 
            ConnectionString = configuration.GetConnectionString("ConnectionStrings");
            // Lấy tên bảng
            TableName = typeof(T).Name;
        }

        public bool CheckDuplicateCode(Guid? entityID, string entityCode)
        {
            using(SqlConnection = new MySqlConnection(ConnectionString))
            {
                // Query proc
                var sqlQuery = $"Proc_CheckDuplicate{TableName}";
                
                // Viết câu lệnh where
                var where = "WHERE";    
                if (entityID != Guid.Empty)
                {
                    where += $" {TableName}ID = '" + entityID + "' AND ";
                }

                where += $" {TableName}Code = '" + entityCode + "' ";

                // Thêm params
                var parameters = new DynamicParameters();
                parameters.Add("$Where", where);

                // Gọi query
                var res = SqlConnection.QueryFirstOrDefault(sql: sqlQuery, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Trả về kết quảp
                if (res.Count == 0)
                {
                    return false;
                } else
                {
                    return true;
                }
            }
        }
        #endregion

        #region Repository
        public IEnumerable<T> Get()
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlQuery = $"Proc_GetAll{TableName}";
                var res = SqlConnection.Query<T>(sql: sqlQuery, commandType: System.Data.CommandType.StoredProcedure).ToList();

                // Trả lại kết quả
                return res;
            }
        }

        public int Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}