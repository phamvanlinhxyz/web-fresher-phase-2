using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng món ăn
    /// </summary>
    /// Created by: linhpv (07/08/2022)
    public class Dish : BaseEntity
    {
        #region Contructor
        public Dish()
        {
            DishID = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DishID { get; set; }
        /// <summary>
        /// Tên món ăn
        /// </summary>
        public string DishName { get; set; }
        /// <summary>
        /// Mã món ăn
        /// </summary>
        public string DishCode { get; set; }
        /// <summary>
        /// ID nhóm thực đơn
        /// </summary>
        public Guid? MenuGroupID { get; set; }
        /// <summary>
        /// Tên nhóm thực đơn
        /// </summary>
        public string? MenuGroupName { get; set; }
        /// <summary>
        /// ID đơn vị tính
        /// </summary>
        public Guid UnitID { get; set; }
        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string? UnitName { get; set; }
        /// <summary>
        /// Giá bán
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// Giá gốc
        /// </summary>
        public decimal? PurchasePrice { get; set; }
        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// ID bếp
        /// </summary>
        public Guid? KitchenID { get; set; }
        /// <summary>
        /// Tên bếp
        /// </summary>
        public string? KitchenName { get; set; }
        /// <summary>
        /// Hiển thị trên menu
        /// </summary>
        public int? ShowOnMenu { get; set; }
        /// <summary>
        /// Là bán thành phẩm
        /// </summary>
        public int? SemiFinishedProduct { get; set; }
        /// <summary>
        /// Đã định lượng nguyên vật liệu
        /// </summary>
        public Enum.MaterialQuantified? MaterialQuantified { get; set; }
        /// <summary>
        /// Danh sách nguyên vật liệu
        /// </summary>
        public List<DishMaterial>? DishMaterials { get; set; }
        /// <summary>
        /// Danh sách nguyên vật liệu bị xóa
        /// </summary>
        public List<Guid>? DeletedDM { get; set; }
        #endregion
    }
}
