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
        /// Service thêm món ăn mới
        /// </summary>
        /// <param name="dish">Món ăn mới</param>
        /// <param name="dishMaterials">Danh sách nguyên vật liệu</param>
        /// <returns>ID món ăn mới</returns>
        /// Created by: linhpv (17/08/2022)
        public Response InsertService(Dish dish);

        /// <summary>
        /// Tự động sinh mã 
        /// </summary>
        /// <param name="DishName">Tên món ăn</param>
        /// <returns>Trả về mã code đã sinh</returns>
        /// Created by: linhpv (12/08/2022)
        public string AutoGenDishCode(string DishName);

        /// <summary>
        /// Xử lý phân trang, lọc dữ liệu
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="filterObjects">Mảng lọc</param>
        /// <returns>Trả về các bản ghi thỏa mãn</returns>
        /// Created by: linhpv (09/08/2022)
        public object PagingService(int pageIndex, int pageSize, FilterObject[] filterObjects); 
    }
}
