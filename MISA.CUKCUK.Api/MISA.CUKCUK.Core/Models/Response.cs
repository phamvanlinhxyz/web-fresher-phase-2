using MISA.CUKCUK.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Model dữ liệu trả về fe
    /// </summary>
    /// Created by: linhpv (15/08/2022)
    public class Response
    {
        #region Contructor
        public Response(object? data, bool success, ErrorCode? errorCode, string? userMsg, string? devMsg)
        {
            Data = data;    
            Success = success;
            ErrorCode = errorCode;
            UserMsg = userMsg;
            DevMsg = devMsg;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Thành công hay không
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Mã lỗi nội bộ
        /// </summary>
        public ErrorCode? ErrorCode { get; set; }

        /// <summary>
        /// Thông báo cho user
        /// </summary>
        public string? UserMsg { get; set; }

        /// <summary>
        /// Thông báo cho dev
        /// </summary>
        public string? DevMsg { get; set; }

        #endregion
    }
}
