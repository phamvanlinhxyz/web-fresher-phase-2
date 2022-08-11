using MISA.CUKCUK.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
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
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public InputType InputType { get; set; }
        public FilterType FilterType { get; set; }
        #endregion
    }
}
