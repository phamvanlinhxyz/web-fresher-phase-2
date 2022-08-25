﻿using MISA.CUKCUK.Core.Enum;
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
    public class UnitService : BaseService<Unit>, IUnitService
    {
        #region Variable
        IUnitRepository _repository;
        #endregion

        #region Contructor
        /// <summary>
        /// Hàm khởi tạo 
        /// </summary>
        /// <param name="repository">repo</param>
        /// Created by: linhpv (15/08/2022)
        public UnitService(IUnitRepository repository) : base(repository)
        {
            _repository = repository;
        }
        #endregion

        #region Override
        /// <summary>
        /// ghi đè phương thức validate
        /// </summary>
        /// <param name="menuGroup">Nhóm thực đơn</param>
        /// <returns>null - nếu valid, thông báo - nếu không valid</returns>
        protected override ErrorCode Validate(Unit unit)
        {
            // Check đơn vị tính trống
            if (string.IsNullOrEmpty(unit.UnitName))
            {   
                return ErrorCode.EmptyUnit;
            }
            // Check trùng đơn vị tính
            if (_repository.CheckDuplicate(Guid.Empty, unit.UnitName, "UnitName"))
            {
                return ErrorCode.DuplicateUnit;
            }
            return ErrorCode.NoError;
        }
        #endregion
    }
}
