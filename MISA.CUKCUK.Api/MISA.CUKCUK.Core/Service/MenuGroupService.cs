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
        protected override string? Validate(MenuGroup menuGroup)
        {
            var langCode = Common.LanguageCode;
            // Check mã nhóm trống
            if (string.IsNullOrEmpty(menuGroup.MenuGroupCode))
            {
                return Resources.Resource.ResourceManager.GetString($"{langCode}_MenuGroupCode_Empty");
            }
            // Check tên nhóm trống
            if (string.IsNullOrEmpty(menuGroup.MenuGroupName))
            {
                return Resources.Resource.ResourceManager.GetString($"{langCode}_MenuGroupName_Empty");
            }
            // Check trùng mã nhóm
            if (_repository.CheckDuplicate(Guid.Empty, menuGroup.MenuGroupCode, "MenuGroupCode"))
            {
                return string.Format(Resources.Resource.ResourceManager.GetString($"{langCode}_Duplicate_MenuGroupCode"), menuGroup.MenuGroupCode);
            }
            // Check trùng tên nhóm
            if (_repository.CheckDuplicate(Guid.Empty, menuGroup.MenuGroupName, "MenuGroupName")) {
                return string.Format(Resources.Resource.ResourceManager.GetString($"{langCode}_Duplicate_MenuGroupName"), menuGroup.MenuGroupName);
            }
            return null;
        }
        #endregion
    }
}
