using MISA.CUKCUK.Core.Enum;
using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Services
{
    /// <summary>
    /// Interface c/ho các service của bảng món ăn
    /// </summary>/
    /// Created by: linhpv (08/08/2022)
    public interface IDishService
    {
        /// <summary>
        /// Service sửa thông tin món ăn
        /// </summary>
        /// <param name="dish">Món ăn cần sửa</param>
        /// <returns>responst</returns>
        /// Created by: linhpv (18/08/2022)
        Response UpdateService(Dish dish);

        /// <summary>
        /// Service thêm món ăn mới
        /// </summary>
        /// <param name="dish">Món ăn mới</param>
        /// <returns>response</returns>
        /// Created by: linhpv (17/08/2022)
        Response InsertService(Dish dish);

        /// <summary>
        /// Tự động sinh mã 
        /// </summary>
        /// <param name="DishName">Tên món ăn</param>
        /// <returns>Trả về mã code đã sinh</returns>
        /// Created by: linhpv (12/08/2022)
        string AutoGenDishCode(string DishName);

        /// <summary>
        /// Xử lý phân trang, lọc dữ liệu
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="filterObjects">Mảng lọc</param>
        /// <returns>Trả về các bản ghi thỏa mãn</returns>
        /// Created by: linhpv (09/08/2022)
        object PagingService(int pageIndex, int pageSize, FilterObject[] filterObjects, string? sortBy, string? sortType); 
    }
}
