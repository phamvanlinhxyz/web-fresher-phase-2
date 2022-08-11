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
                // Nếu loại đầu vào là chữ
                if (filterObject.InputType == Enum.InputType.Text)
                {
                    // Xử lý kiểu lọc
                    switch (filterObject.FilterType)
                    {
                        // Chứa
                        case Enum.FilterType.Contain:
                            where += filterObject.ColumnName + " LIKE '%" + filterObject.Value + "%' ";
                            break;
                        // Bằng
                        case Enum.FilterType.Equal:
                            where += filterObject.ColumnName + " = '" + filterObject.Value + "' ";
                            break;
                        // Bắt đầu bằng
                        case Enum.FilterType.StartWith:
                            where += filterObject.ColumnName + " LIKE '" + filterObject.Value + "%' ";
                            break;
                        // Kết thúc bằng
                        case Enum.FilterType.EndWith:
                            where += filterObject.ColumnName + " LIKE '%" + filterObject.Value + "' ";
                            break;
                        // Không chứa
                        case Enum.FilterType.NotContain:
                            where += filterObject.ColumnName + " NOT LIKE '%" + filterObject.Value + "%' ";
                            break;
                    }
                } else if (filterObject.InputType == Enum.InputType.Number) {
                    // Nếu loại đầu vào là số
                    switch (filterObject.FilterType)
                    {
                        case Enum.FilterType.Equal:
                            where += filterObject.ColumnName + " = '" + filterObject.Value + "' ";
                            break;
                        case Enum.FilterType.Less:
                            where += filterObject.ColumnName + " < '" + filterObject.Value + "' ";
                            break;
                        case Enum.FilterType.LessOrEqual:
                            where += filterObject.ColumnName + " <= '" + filterObject.Value + "' ";
                            break;
                        case Enum.FilterType.Greater:
                            where += filterObject.ColumnName + " > '" + filterObject.Value + "' ";
                            break;
                        case Enum.FilterType.GreaterOrEqual:
                            where += filterObject.ColumnName + " >= '" + filterObject.Value + "' ";
                            break;
                        default:
                            break;
                    }
                }
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
