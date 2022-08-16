using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng đồ ăn - nguyên vật liệu
    /// </summary>
    /// Created by: linhpv (08/08/2022)
    public class DishMaterial : BaseEntity
    {
        #region Contructor
        public DishMaterial()
        {
            DishMaterialID = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DishMaterialID { get; set; }
        /// <summary>
        /// ID món ăn
        /// </summary>
        public Guid DishID { get; set; }
        /// <summary>
        /// Tên món ăn
        /// </summary>
        public string? DishName { get; set; }
        /// <summary>
        /// ID nguyên vật liệu
        /// </summary>
        public Guid MaterialID { get; set; }
        /// <summary>
        /// Tên nguyên vật liệu
        /// </summary>
        public string? MaterialName { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal? MaterialAmount { get; set; }
        /// <summary>
        /// Giá vốn 
        /// </summary>
        public decimal? MaterialPurchasePrice { get; set; }
        /// <summary>
        /// Thành tiền
        /// </summary>
        public decimal? TotalPrice { get; set; }
        #endregion
    }
}
