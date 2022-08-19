using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Service
{
    public class MaterialService : BaseService<Material>, IMaterialService
    {
        #region Variable
        IMaterialRepository _repository;
        #endregion

        #region Contructor
        public MaterialService(IMaterialRepository repository) : base(repository)
        {
            _repository = repository;
        }
        #endregion

        #region Override
        /// <summary>
        /// Ghi đè lại hàm validate 
        /// </summary>
        /// <param name="entity">bản ghi cần validate</param>
        /// <returns>null - nếu valid, lỗi - nếu không valid</returns>
        protected override string? Validate(Material entity)
        {
            var langCode = Common.LanguageCode;
            string errorMsg = "";
            // Check mã NVL trống
            if (string.IsNullOrEmpty(entity.MaterialCode))
            {
                errorMsg += Resources.Resource.ResourceManager.GetString($"{langCode}_MaterialCode_Empty");
            }
            // Check tên NVL trống
            if (string.IsNullOrEmpty(entity.MaterialName))
            {   
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    errorMsg += "; ";
                }
                errorMsg += Resources.Resource.ResourceManager.GetString($"{langCode}_MaterialName_Empty");
            }
            // Check đơn vị tính trống
            if (entity.UnitID == Guid.Empty)
            {
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    errorMsg += "; ";
                }
                errorMsg += Resources.Resource.ResourceManager.GetString($"{langCode}_Unit_Empty");
            }
            // Check trùng mã NVL
            if (_repository.CheckDuplicate(Guid.Empty, entity.MaterialCode, "MaterialCode"))
            {
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    errorMsg += "; ";
                }
                errorMsg += string.Format(Resources.Resource.ResourceManager.GetString($"{langCode}_Duplicate_MaterialCode"), entity.MaterialCode);
            }
            return errorMsg;
        }
        #endregion
    }
}
