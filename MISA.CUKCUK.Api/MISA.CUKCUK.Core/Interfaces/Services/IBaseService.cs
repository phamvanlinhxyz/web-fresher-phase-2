﻿using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Services
{
    /// <summary>
    /// Interface cơ sở cho các interface service
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: linhpv (07/08/2022)
    public interface IBaseService<T>
    {
        #region Interface
        /// <summary>
        /// Interface thêm bản ghi mới
        /// </summary>
        /// <param name="entity">bản ghi mới</param>
        /// <returns>Số bản ghi đã đc thêm</returns>
        /// Created by: linhpv (07/08/2022)
        Response InsertService(T entity);
        #endregion
    }
}
