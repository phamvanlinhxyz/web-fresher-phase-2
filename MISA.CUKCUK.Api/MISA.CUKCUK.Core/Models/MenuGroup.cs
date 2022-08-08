using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng nhóm thực đơn
    /// </summary>
    /// Created by: linhpv (07/08/2022)
    public class MenuGroup : BaseEntity
    {
        #region Contructor
        public MenuGroup()
        {
            MenuGroupID = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid MenuGroupID { get; set; }
        /// <summary>
        /// Mã nhóm thực đơn
        /// </summary>
        public string MenuGroupCode { get; set; }
        /// <summary>
        /// Tên nhóm thực đơn
        /// </summary>
        public string MenuGroupName { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// ID bếp
        /// </summary>
        public Guid KitchenID { get; set; }
        /// <summary>
        /// Tên bếp
        /// </summary>
        public string? KitchenName { get; set; }
        #endregion
    }
}
