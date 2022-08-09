using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Service
{
    /// <summary>
    /// Dish service
    /// </summary>
    public class DishService : BaseService<Dish>, IDishService
    {
        #region Variable
        IDishRepository _repository;
        #endregion

        #region Contructor
        public DishService(IDishRepository repository) : base(repository)
        {
            _repository = repository;   
        }
        #endregion

        #region Service
        /// <summary>
        /// Xử lý phân trang, lọc dữ liệu
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="filterObjects">Mảng lọc</param>
        /// <returns>Trả về các bản ghi thỏa mãn</returns>
        /// Created by: linhpv (09/08/2022)
        public object PagingService(int pageIndex, int pageSize, FilterObject[] filterObjects)
        {
            // Xử lý số trang đầu vào
            if (string.IsNullOrEmpty(pageIndex.ToString()) || pageIndex <= 0)
            {
                pageIndex = 1;
            }

            // Xử lý số bản ghi trên 1 trang
            if (string.IsNullOrEmpty(pageSize.ToString()) || pageSize <= 0)
            {
                pageSize = 1;
            }

            // Xử lý tìm câu lệnh tìm kiếm
            string where = "";
            for (int i = 0; i < filterObjects.Length; i++)
            {
                FilterObject filterObject = filterObjects[i];
                where += filterObject.ColumnName + " LIKE '%" + filterObject.Value + "%' ";
                if (i < filterObjects.Length - 1)
                {
                    where += "AND ";
                }
            }

            // Gọi repository
            var data = _repository.Paging(pageIndex, pageSize, where);

            // Trả về data
            return data;
        }
        #endregion
    }
}
