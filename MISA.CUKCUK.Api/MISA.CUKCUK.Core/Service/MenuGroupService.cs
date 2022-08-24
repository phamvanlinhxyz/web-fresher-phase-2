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
    public class MenuGroupService : BaseService<MenuGroup>, IMenuGroupService
    {
        #region Variable
        IMenuGroupRepository _repository;
        #endregion

        #region Contructor
        /// <summary>
        /// Hàm khởi tạo 
        /// </summary>
        /// <param name="repository">repo</param>
        /// Created by: linhpv (15/08/2022)
        public MenuGroupService(IMenuGroupRepository repository) : base(repository)
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
        protected override ErrorCode Validate(MenuGroup menuGroup)
        {
            // Check mã nhóm trống
            if (string.IsNullOrEmpty(menuGroup.MenuGroupCode))
            {
                return ErrorCode.EmptyCode;
            }
            // Check tên nhóm trống
            if (string.IsNullOrEmpty(menuGroup.MenuGroupName))
            {
                return ErrorCode.EmptyName;
            }
            // Check trùng mã nhóm
            if (_repository.CheckDuplicate(Guid.Empty, menuGroup.MenuGroupCode, "MenuGroupCode"))
            {
                return ErrorCode.DuplicateCode;
            }
            // Check trùng tên nhóm
            if (_repository.CheckDuplicate(Guid.Empty, menuGroup.MenuGroupName, "MenuGroupName")) {
                return ErrorCode.DuplicateName;
            }
            return ErrorCode.NoError;
        }
        #endregion
    }
}
