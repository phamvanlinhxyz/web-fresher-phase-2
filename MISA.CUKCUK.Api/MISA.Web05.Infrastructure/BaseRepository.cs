using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MySqlConnector;
using System.Data;

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

        /// <summary>
        /// Check trùng
        /// </summary>
        /// <param name="entityID">Id bản ghi</param>
        /// <param name="text">Dữ liệu cần check</param>
        /// <param name="column">Cột cần check</param>
        /// <returns>true - nếu trùng, false - nếu không trùng</returns>
        /// Created by: linhpv (14/08/2022)
        public bool CheckDuplicate(Guid? entityID, string text, string column)
        {
            using(SqlConnection = new MySqlConnection(ConnectionString))
            {
                // Query proc
                var sqlQuery = $"Proc_CheckDuplicate";
                
                // Viết câu lệnh where
                var where = "WHERE";    
                if (entityID != Guid.Empty)
                {
                    where += $" {TableName}ID <> '" + entityID + "' AND ";
                }

                where += $" {column} = '" + text + "' ";

                // Thêm params
                var parameters = new DynamicParameters();
                parameters.Add("$Where", where);
                parameters.Add("$TableName", TableName);

                // Gọi query
                var res = SqlConnection.QueryFirstOrDefault(sql: sqlQuery, param: parameters, commandType: CommandType.StoredProcedure);

                // Trả về kết quả
                if (res.Count == 0)
                {
                    return false;
                } 
                else
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

        public virtual Guid Insert(T entity)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                var sqlQuery = $"Proc_Insert{TableName}";

                // Add params
                var parameters = new DynamicParameters(entity);
                parameters.Add("$newID", dbType: DbType.Guid, direction: ParameterDirection.Output);

                // Lấy kết quả
                bool isSuccess = SqlConnection.Execute(sql: sqlQuery, param: parameters, commandType: CommandType.StoredProcedure) > 0;
                var newID = parameters.Get<Guid>("$newID");

                // Nếu thành công trả về ID mới ngược lại trả về ID trông
                if (isSuccess)
                {
                    return newID;
                } 
                else
                {
                    return Guid.Empty;
                }
            }
        }
        #endregion
    }
}