using MISA.CUKCUK.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Đối tượng lọc
    /// </summary>
    /// Created by: linhpv (11/08/2022)
    public class FilterObject
    {
        #region Contructor
        public FilterObject(string columnName, string value, InputType inputType, FilterType filterType)
        {
            this.ColumnName = columnName;
            this.Value = value;
            this.InputType = inputType;
            this.FilterType = filterType;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Tên cột
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Kiểu input
        /// </summary>
        public InputType InputType { get; set; }

        /// <summary>
        /// Kiểu lọc
        /// </summary>
        public FilterType FilterType { get; set; }
        #endregion
    }
}
