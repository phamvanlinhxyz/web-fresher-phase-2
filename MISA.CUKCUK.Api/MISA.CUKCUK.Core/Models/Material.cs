using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng nguyên vật liệu
    /// </summary>
    /// Created by: linhpv (07/08/2022)
    public class Material : BaseEntity
    {
        #region Contructor
        public Material()
        {
            MaterialID = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid MaterialID { get; set; }

        /// <summary>
        /// Mã nguyên vật liệu
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// Tên nguyên vật liệu
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// ID đơn vị tính
        /// </summary>
        public Guid UnitID { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string? UnitName { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }
        #endregion
    }
}
