using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface của dish repository
    /// </summary>
    /// Created by: linhpv (09/08/2022)
    public interface IDishRepository
    {
        /// <summary>
        /// Thêm món ăn mới
        /// </summary>
        /// <param name="dish">Món ăn cần thêm</param>
        /// <param name="dishMaterials">Các định lượng nguyên vật liệu</param>
        /// <returns>Trả về ID món ăn mới</returns>
        /// Created by: linhpv (17/08/2022)
        Guid Insert(Dish dish);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entityID">ID bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: linhpv (11/08/2022)
        int Delete(Guid entityID);

        /// <summary>
        /// Phân trang, lọc
        /// </summary>
        /// <param name="pageIndex">số trang</param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <param name="where">câu lệnh điều kiện</param>
        /// <returns>trả về phân trang</returns>
        /// Created by: linhpv (09/08/2022)
        object Paging(int pageIndex, int pageSize, string where);
    }
}
