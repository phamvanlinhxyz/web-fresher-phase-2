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
        public Response(dynamic? data, bool success, ErrorCode? errorCode, string? userMsg, string? devMsg)
        {
            Data = data;    
            Success = success;
            ErrorCode = errorCode;
            UserMsg = userMsg;
            DevMsg = devMsg;
        }
        #endregion

        #region Properties
        public dynamic? Data { get; set; }
        public bool Success { get; set; }
        public ErrorCode? ErrorCode { get; set; }
        public string? UserMsg { get; set; }
        public string? DevMsg { get; set; }

        #endregion
    }
}
