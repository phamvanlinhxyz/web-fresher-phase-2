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
        public FilterObject(string ColumnName, string Value)
        {
            this.ColumnName = ColumnName;
            this.Value = Value;
        }
        #endregion

        #region Properties
        public string ColumnName { get; set; }
        public string Value { get; set; }
        #endregion
    }
}
