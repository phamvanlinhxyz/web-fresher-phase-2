using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng đơn vị tính
    /// </summary>
    /// Created by: linhpv (07/08/2022)
    public class Unit : BaseEntity
    {
        #region Contructor
        public Unit()
        {
            UnitID = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid UnitID { get; set; }
        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }
        #endregion
    }
}
