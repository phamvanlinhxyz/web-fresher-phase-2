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
        /// Thuộc tính nguyên vật liệu
        /// </summary>
        public string MateralProperty { get; set; }
        /// <summary>
        /// ID nhóm nguyên vật liệu
        /// </summary>
        public Guid? MaterialGroupID { get; set; }
        /// <summary>
        /// Tên nhóm nguyên vật liệu
        /// </summary>
        public string? MaterialGroupName { get; set; }
        /// <summary>
        /// ID kho
        /// </summary>
        public Guid? RepositoryID { get; set; }
        /// <summary>
        /// Tên kho
        /// </summary>
        public string? RepositoryName { get; set; }
        /// <summary>
        /// Thời gian hết hạn
        /// </summary>
        public int? Expiry { get; set; }
        /// <summary>
        /// Loại thời gian: ngày, tháng, năm
        /// </summary>
        public int? ExpiryType { get; set; }
        /// <summary>
        /// ID đơn vị tính
        /// </summary>
        public Guid UnitID { get; set; }
        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string? UnitName { get; set; }
        /// <summary>
        /// Tồn kho tối thiểu
        /// </summary>
        public decimal? MinumumInvetory { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }
        #endregion
    }
}
