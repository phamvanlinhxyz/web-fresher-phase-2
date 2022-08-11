﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Repositories
{
    /// <summary>
    /// Interface cơ sở cho các interface repo
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: linhpv (07/08/2022)
    public interface IBaseRepository<T>
    {
        #region Interface
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: linhpv (07/08/2022)
        IEnumerable<T> Get();
        /// <summary>
        /// Thêm bản ghi mới
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// <returns>Số bản ghi đã thêm</returns>
        /// Created by: linhpv (07/08/2022)
        int Insert(T entity);
        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần sửa</param>
        /// <returns>Số bản ghi đã sửa</returns>
        /// Created by: linhpv (07/08/2022)
        int Update(T entity);
        #endregion
    }
}
