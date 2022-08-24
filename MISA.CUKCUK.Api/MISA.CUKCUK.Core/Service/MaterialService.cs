using MISA.CUKCUK.Core.Enum;
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
        /// Created by: linhpv (22/08/2022)
        protected override ErrorCode Validate(Material entity)
        {
            // Check mã NVL trống
            if (string.IsNullOrEmpty(entity.MaterialCode))
            {
                return ErrorCode.EmptyCode;
            }
            // Check tên NVL trống
            if (string.IsNullOrEmpty(entity.MaterialName))
            {   
                return ErrorCode.EmptyName;
            }
            // Check đơn vị tính trống
            if (entity.UnitID == Guid.Empty)
            {
                return ErrorCode.EmptyUnit;
            }
            // Check trùng mã NVL
            if (_repository.CheckDuplicate(Guid.Empty, entity.MaterialCode, "MaterialCode"))
            {
                return ErrorCode.DuplicateCode;
            }
            return ErrorCode.NoError;
        }
        #endregion
    }
}
