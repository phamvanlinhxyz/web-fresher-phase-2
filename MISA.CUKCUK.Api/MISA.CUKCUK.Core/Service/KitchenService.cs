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
    public class KitchenService : BaseService<Kitchen>, IKitchenService
    {
        #region Variable
        IKitchenRepository _repository;
        #endregion

        #region Contructor
        public KitchenService(IKitchenRepository repository) : base(repository)
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
        protected override ErrorCode Validate(Kitchen entity)
        {
            // Check tên bếp trống
            if (string.IsNullOrEmpty(entity.KitchenName))
            {
                return ErrorCode.EmptyName;
            }
            return ErrorCode.NoError;
        }
        #endregion
    }
}
