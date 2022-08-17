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
using static Dapper.SqlMapper;

namespace MISA.Web05.Infrastructure
{
    public class DishRepository : IDishRepository
    {
        #region Variable
        protected string ConnectionString;
        protected MySqlConnection SqlConnection;
        #endregion

        #region Contructor
        public DishRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("ConnectionStrings");
        }
        #endregion

        #region Repository
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
        /// Thêm món ăn mới
        /// </summary>
        /// <param name="dish">Món ăn mới</param>
        /// <param name="dishMaterials">Danh sách nguyên vật liệu</param>
        /// <returns>ID món ăn mới</returns>
        /// Created by: linhpv (17/08/2022)
        public Guid Insert(Dish dish)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                SqlConnection.Open();

                // Khởi tạo transaction
                using (var transaction = SqlConnection.BeginTransaction())
                {
                    try
                    {
                        var sqlQuery = "Proc_InsertDish";

                        // Lấy ra nguyên vật liệu
                        List<DishMaterial>? dishMaterials = dish.DishMaterials;
                        dish.DishMaterials = null;

                        // Add params
                        var parameters = new DynamicParameters(dish);
                        parameters.Add("$newID", dbType: DbType.Guid, direction: ParameterDirection.Output);

                        // Lấy kết quả
                        SqlConnection.Query<Dish>(sql: sqlQuery, param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                        var newID = parameters.Get<Guid>("$newID");

                        // Kiểm tra nguyên vật liệu
                        if (dishMaterials != null && dishMaterials.Count > 0)
                        {
                            // Nếu có nguyên vật liệu thì thêm
                            var sqlQueryMaterial = "Proc_InsertDishMaterial";

                            foreach (DishMaterial dishMaterial in dishMaterials) 
                            {
                                // Tạo param
                                var param = new DynamicParameters();
                                param.Add("$DishID", newID);
                                param.Add("$MaterialID", dishMaterial.MaterialID);
                                param.Add("$MaterialAmount", dishMaterial.MaterialAmount);
                                param.Add("$MaterialPurchasePrice", dishMaterial.MaterialPurchasePrice);
                                param.Add("$TotalPrice", dishMaterial.MaterialAmount * dishMaterial.MaterialPurchasePrice);

                                // Query
                                SqlConnection.Query<DishMaterial>(sql: sqlQueryMaterial, param: param, transaction: transaction, commandType: CommandType.StoredProcedure);
                            }
                        }

                        // Nếu thêm tất cả thành công thì mới Commit
                        transaction.Commit();
                        // Trả về kết quả
                        return newID;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
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
                var dishs = SqlConnection.Query(sql: sqlQuery, param: parameters, commandType: CommandType.StoredProcedure).ToList();

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
        #endregion
    }
}
