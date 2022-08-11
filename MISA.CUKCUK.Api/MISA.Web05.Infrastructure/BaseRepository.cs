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