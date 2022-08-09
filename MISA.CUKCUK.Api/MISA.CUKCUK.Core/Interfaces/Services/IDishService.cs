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
    public interface IDishService : IBaseService<Dish>
    {
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
