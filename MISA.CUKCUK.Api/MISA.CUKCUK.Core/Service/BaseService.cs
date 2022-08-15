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
    /// Lớp service cha
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: linhpv (08/08/2022)
    public class BaseService<T> : IBaseService<T>
    {
        #region Variable
        IBaseRepository<T> _repository;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Service
        /// <summary>
        /// Thêm bản ghi mới
        /// </summary>
        /// <param name="entity">Bản ghi cần thêm</param>
        /// <returns>Trả về ID mới</returns>
        /// Created by: linhpv (12/08/2022)
        public Response InsertService(T entity)
        {
            // Validate dữ liệu
            string valid = Validate(entity);

            if (string.IsNullOrEmpty(valid)) {
                return new Response(data: _repository.Insert(entity), success: true, errorCode: Enum.ErrorCode.NoError, userMsg: "", devMsg: "");
            } 
            else
            {
                return new Response(data: null, success: false, errorCode: Enum.ErrorCode.BadRequest, userMsg: valid, devMsg: valid);
            }
        }
        #endregion

        #region Function
        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">Bản ghi cần validate</param>
        /// <returns>null - valid, thông báo lỗi - not valid</returns>
        protected virtual string? Validate(T entity)
        {
            return null;
        }
        #endregion
    }
}
