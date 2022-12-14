using System;
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
        /// Check trùng
        /// </summary>
        /// <param name="entityID">Id bản ghi</param>
        /// <param name="text">Dữ liệu cần check</param>
        /// <param name="column">Cột cần check</param>
        /// <returns>true - nếu trùng, false - nếu không trùng</returns>
        /// Created by: linhpv (15/08/2022)
        bool CheckDuplicate(Guid? entityID, string text, string column);
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
        Guid Insert(T entity);
        #endregion
    }
}
