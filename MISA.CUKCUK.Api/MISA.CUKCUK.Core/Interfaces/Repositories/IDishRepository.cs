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
    public interface IDishRepository : IBaseRepository<Dish>
    {
        /// <summary>
        /// Cập nhật món ăn
        /// </summary>
        /// <param name="dish">Món ăn cần cập nhật</param>
        /// <returns>ID món ăn đã cập nhật</returns>
        /// Created by: linhpv (18/08/2022)
        Guid Update(Dish dish);

        /// <summary>
        /// Lấy danh sách nguyên vật liệu
        /// </summary>
        /// <param name="dishID">Id món ăn </param>
        /// <returns>Danh sách nguyên vật liệu</returns>
        /// Created by: linhpv (17/08/2022)
        List<DishMaterial> GetListMaterial(Guid dishID);

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
        object Paging(int pageIndex, int pageSize, string where, string? sort);
    }
}
