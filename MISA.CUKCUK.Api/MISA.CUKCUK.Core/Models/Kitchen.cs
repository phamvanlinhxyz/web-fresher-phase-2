using MISA.CUKCUK.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Models
{
    /// <summary>
    /// Bảng bếp
    /// </summary>
    /// Created by: linhpv (07/08/2022)
    public class Kitchen : BaseEntity
    {
        #region Contructor
        public Kitchen()
        {
            KitchenID = Guid.NewGuid();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid KitchenID { get; set; }

        /// <summary>
        /// Tên bếp
        /// </summary>
        public string KitchenName { get; set; }

        /// <summary>
        /// Thiết bị sử dụng
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Device? Device { get; set; }

        /// <summary>
        ///  Tên thiết bị sử dụng
        /// </summary>
        public string? DeviceName { 
            get 
            {
                var langCode = Common.LanguageCode;
                switch(Device)
                {
                    case Enum.Device.Tablet:
                        return Resources.Resource.ResourceManager.GetString($"{langCode}_Device_Tablet");
                    case Enum.Device.Printer:
                        return Resources.Resource.ResourceManager.GetString($"{langCode}_Device_Printer");
                    default:
                        return Resources.Resource.ResourceManager.GetString($"{langCode}_Device_No");
                }
            } 
        }

        /// <summary>
        /// Diễn giải
        /// </summary>
        public string? Description { get; set; }
        #endregion
    }
}
