using MISA.CUKCUK.Core.Enum;
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
        public virtual Response InsertService(T entity)
        {
            // Validate dữ liệu
            ErrorCode error = Validate(entity);

            // Nếu valid thì gọi repo insert bản ghi mới và lấy ID mới
            if (error == ErrorCode.NoError) {
                var newID = _repository.Insert(entity);
                // Check ID mới trả về
                if (newID != Guid.Empty)
                {
                    return new Response(newID, true, error, "", "");
                }
                error = ErrorCode.AddFailed;
            }

            // Các trường hợp lỗi thì trả về lỗi
            return new Response(null, false, error, "", "");
        }
        #endregion

        #region Function
        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="entity">Bản ghi cần validate</param>
        /// <returns>null - valid, thông báo lỗi - not valid</returns>
        protected virtual ErrorCode Validate(T entity)
        {
            return ErrorCode.NoError;
        }
        #endregion
    }
}
