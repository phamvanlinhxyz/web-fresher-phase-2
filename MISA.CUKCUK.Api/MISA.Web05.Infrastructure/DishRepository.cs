﻿using Dapper;
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
    public class DishRepository : BaseRepository<Dish>, IDishRepository
    {
        #region Contructor
        public DishRepository(IConfiguration configuration) : base(configuration)
        {
        }
        #endregion

        #region Repository
        /// <summary>
        /// Sửa món ăn
        /// </summary>
        /// <param name="dish">Món ăn cần sửa</param>
        /// <returns>Id món ăn đã sửa</returns>
        /// Created by: linhpv (18/08/2022)
        public Guid Update(Dish dish)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // Kiểm tra đã mở connection hay chưa
                if (SqlConnection.State != ConnectionState.Open)
                {
                    SqlConnection.Open();
                }

                // Khởi tạo transaction
                using (var transaction = SqlConnection.BeginTransaction())
                {
                    try
                    {
                        // Query và params
                        var sqlQuery = "Proc_UpdateDish";

                        // Lấy danh sách nguyên vật liệu
                        List<DishMaterial>? dishMaterials = dish.DishMaterials;
                        // Lấy danh sách nguyên vật liệu bị xóa
                        List<Guid>? deletedDM = dish.DeletedDM;
                        // Set các danh sách = null
                        dish.DishMaterials = null;
                        dish.DeletedDM = null;

                        var parameters = new DynamicParameters(dish);

                        // Query và lấy kết quả
                        bool isSuccess = SqlConnection.Execute(sql: sqlQuery, param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure) > 0;

                        // Nếu đã sửa thành công thông tin món ăn
                        if (isSuccess)
                        {
                            // Xóa những nguyên vật liệu đã bỏ
                            // Kiểm tra danh sách xóa nguyên vật liệu có null hay không
                            if (deletedDM != null && deletedDM.Count > 0)
                            {
                                sqlQuery = "Proc_DeleteDMByID";
                                foreach (Guid DMID in deletedDM)
                                {
                                    parameters = new DynamicParameters();
                                    parameters.Add("$DMID", DMID);

                                    isSuccess = SqlConnection.Execute(sql: sqlQuery, param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure) > 0;
                                    // Nếu không thành công thì rollback
                                    if (!isSuccess)
                                    {
                                        transaction.Rollback();
                                        return Guid.Empty;
                                    }
                                }
                            }
                            
                            // Duyệt danh sách định lượng nguyên vật liệu
                            // Kiểm tra nguyên vật liệu có trống hay không
                            if (dishMaterials != null && dishMaterials.Count > 0)
                            {
                                foreach (DishMaterial dishMaterial in dishMaterials)
                                {
                                    // Add parameter
                                    parameters = new DynamicParameters();
                                    parameters.Add("$DishID", dish.DishID);
                                    parameters.Add("$MaterialID", dishMaterial.MaterialID);
                                    parameters.Add("$MaterialAmount", dishMaterial.MaterialAmount);
                                    parameters.Add("$MaterialPurchasePrice", dishMaterial.MaterialPurchasePrice);
                                    parameters.Add("$TotalPrice", dishMaterial.MaterialAmount * dishMaterial.MaterialPurchasePrice);

                                    // Check định lượng nguyên vật liệu đã được gán id món ăn chưa
                                    // Nếu chưa thì thêm mới, rồi thì cập nhật
                                    if (dishMaterial.DishID == Guid.Empty)
                                    {
                                        sqlQuery = "Proc_InsertDishMaterial"; 
                                    } 
                                    else
                                    {
                                        sqlQuery = "Proc_UpdateDishMaterial";
                                        parameters.Add("$DishMaterialID", dishMaterial.DishMaterialID);
                                    }
                                    // Query
                                    isSuccess = SqlConnection.Execute(sql: sqlQuery, param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure) > 0;
                                    // Nếu không thành công thì Rollback
                                    if (!isSuccess)
                                    {
                                        transaction.Rollback();
                                        return Guid.Empty;
                                    }
                                }
                            }
                            transaction.Commit();
                            return dish.DishID;
                        } 
                        else
                        {
                            transaction.Rollback();
                            return Guid.Empty;
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        //return Guid.Empty;
                        throw;
                    }
                    finally
                    {
                        if (SqlConnection.State != ConnectionState.Closed)
                        {
                            SqlConnection.Close();
                        }
                    }
                    
                }
            }
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
                if (SqlConnection.State != ConnectionState.Open) {
                    SqlConnection.Open();
                }

                // Khởi tạo transaction
                using (var transaction = SqlConnection.BeginTransaction())
                {
                    try 
                    {
                        // Query và params
                        var sqlQuery = "Proc_DeleteDish";
                        var parameters = new DynamicParameters();
                        parameters.Add("$DishID", entityID);

                        // Kiểm tra có xóa thành công không
                        bool isSuccess = SqlConnection.Execute(sql: sqlQuery, param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure) > 0;
                        
                        // Nếu đã xóa thành công món ăn thì xóa hết các định lượng nguyên vật liệu
                        if (isSuccess) 
                        {
                            sqlQuery = "Proc_DeleteMaterialByDish";
                            SqlConnection.Execute(sql: sqlQuery, param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                            return 0;
                        }
                        // Trả về kết quả
                        return 1;
                    }
                    catch (Exception) 
                    {
                        transaction.Rollback();
                        return 0;
                    }
                    finally
                    {
                        if (SqlConnection.State != ConnectionState.Closed)
                        {
                            SqlConnection.Close();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Lấy danh sách nguyên vật liệu theo ID món
        /// </summary>
        /// <param name="dishID">ID món ăn</param>
        /// <returns>Danh sách nguyên vật liệu</returns>
        /// Created by: linhpv(17/08/2022)
        public List<DishMaterial> GetListMaterial(Guid dishID)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // Khởi tạo query và param
                var sqlQuery = "Proc_GetMaterialByDish";
                var parameters = new DynamicParameters();
                parameters.Add("$DishID", dishID);

                // Lấy kết quả
                var res = SqlConnection.Query<DishMaterial>(sql: sqlQuery, param: parameters, commandType: CommandType.StoredProcedure);

                // Trả về controller
                return res.ToList();
            }
        }

        /// <summary>
        /// Thêm món ăn mới
        /// </summary>
        /// <param name="dish">Món ăn mới</param>
        /// <param name="dishMaterials">Danh sách nguyên vật liệu</param>
        /// <returns>ID món ăn mới</returns>
        /// Created by: linhpv (17/08/2022)
        public override Guid Insert(Dish dish)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                if (SqlConnection.State != ConnectionState.Open)
                {
                    SqlConnection.Open();
                }

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
                        bool isSuccess = SqlConnection.Execute(sql: sqlQuery, param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure) > 0;
                        var newID = parameters.Get<Guid>("$newID");
                        // Nếu thêm món ăn thành công thì mới thêm tiếp định lượng nguyên vật liệu
                        if (isSuccess)
                        {
                            // Kiểm tra nguyên vật liệu
                            if (dishMaterials != null && dishMaterials.Count > 0)
                            {
                                // Nếu có nguyên vật liệu thì thêm
                                sqlQuery = "Proc_InsertDishMaterial";

                                foreach (DishMaterial dishMaterial in dishMaterials)
                                {
                                    // Tạo param
                                    parameters = new DynamicParameters();
                                    parameters.Add("$DishID", newID);
                                    parameters.Add("$MaterialID", dishMaterial.MaterialID);
                                    parameters.Add("$MaterialAmount", dishMaterial.MaterialAmount);
                                    parameters.Add("$MaterialPurchasePrice", dishMaterial.MaterialPurchasePrice);
                                    parameters.Add("$TotalPrice", dishMaterial.MaterialAmount * dishMaterial.MaterialPurchasePrice);

                                    // Query
                                    isSuccess = SqlConnection.Execute(sql: sqlQuery, param: parameters, transaction: transaction, commandType: CommandType.StoredProcedure) > 0;

                                    // Nếu không thành công thì Rollback
                                    if (!isSuccess)
                                    {
                                        transaction.Rollback();
                                        return Guid.Empty;
                                    }
                                }
                            }
                            transaction.Commit();
                        } 
                        else
                        {
                            newID = Guid.Empty;
                            transaction.Rollback();
                        }

                        // Trả về kết quả
                        return newID;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (SqlConnection.State != ConnectionState.Closed)
                        {
                            SqlConnection.Close();
                        }
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
        public object Paging(int pageIndex, int pageSize, string where, string? sort)
        {
            using (SqlConnection = new MySqlConnection(ConnectionString))
            {
                // Query và param
                var sqlQuery = "Proc_GetDishsByPaging";
                var parameters = new DynamicParameters();
                parameters.Add("$pageIndex", pageIndex);
                parameters.Add("$pageSize", pageSize);
                parameters.Add("$where", where);
                parameters.Add("$Sort", sort);
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
