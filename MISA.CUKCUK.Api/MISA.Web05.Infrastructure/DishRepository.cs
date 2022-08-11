using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructure
{
    public class DishRepository : BaseRepository<Dish>, IDishRepository
    {
        public DishRepository(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entityID">ID bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: linhpv (11/08/2022)
        public int Delete(Guid entityID)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // Query và params
                var sqlQuery = "Proc_DeleteDish";
                var parameters = new DynamicParameters();
                parameters.Add("$DishID", entityID);

                // Lấy kết quả
                var res = SqlConnection.Execute(sql: sqlQuery, param: parameters, commandType: CommandType.StoredProcedure);

                // Trả về kết quả
                return res;
            }
        }

        /// <summary>
        /// Phân trang, lọc
        /// </summary>
        /// <param name="pageIndex">số trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <param name="where">câu lệnh điều kiện</param>
        /// <returns>trả về phân trang</returns>
        /// Created by: linhpv (09/08/2022)
        public object Paging(int pageIndex, int pageSize, string where)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // Query và param
                var sqlQuery = "Proc_GetDishsByPaging";
                var parameters = new DynamicParameters();
                parameters.Add("$pageIndex", pageIndex);
                parameters.Add("$pageSize", pageSize);
                parameters.Add("$where", where);
                parameters.Add("$Sort", "");
                parameters.Add("$TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("$TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

                // Kết quả
                var dishs = SqlConnection.Query(sql: sqlQuery,param: parameters, commandType: CommandType.StoredProcedure).ToList();

                // Các biến đầu ra
                var totalRecord = parameters.Get<int>("$TotalRecord");
                var totalPage = parameters.Get<int>("$TotalPage");

                // Trả lại kết quả
                var result = new
                {
                    totalPage = totalPage,
                    totalRecord = totalRecord,
                    data = dishs,
                };
                return result;
            }
        }
    }
}
