using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Service
{
    /// <summary>
    /// Dish service
    /// </summary>
    public class DishService : BaseService<Dish>, IDishService
    {
        #region Variable
        IDishRepository _repository;
        #endregion

        #region Contructor
        public DishService(IDishRepository repository) : base(repository)
        {
            _repository = repository;   
        }
        #endregion

        #region Service
        /// <summary>
        /// Tự động sinh mã 
        /// </summary>
        /// <param name="DishName">Tên món ăn</param>
        /// <returns>Trả về mã code đã sinh</returns>
        /// Created by: linhpv (12/08/2022)
        public string AutoGenDishCode(string DishName)
        {
            string[] words  = DishName.Split(" ");
            string newCode = "";

            foreach (var word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    newCode += word[0];
                }
            }
            newCode = RemoveVietnameseTone(newCode).ToUpper();
            if (_repository.CheckDuplicateCode(Guid.Empty, newCode))
            {
                newCode = "";
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        newCode += word[0];
                        if (word.Length > 1)
                        {
                            newCode += word[1];
                        }
                    }
                }

                if (_repository.CheckDuplicateCode(Guid.Empty, newCode))
                {
                    newCode = DishName.Replace(" ", "");
                } 
            } 
            return RemoveVietnameseTone(newCode).ToUpper();
        }

        /// <summary>
        /// Xử lý phân trang, lọc dữ liệu
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="filterObjects">Mảng lọc</param>
        /// <returns>Trả về các bản ghi thỏa mãn</returns>
        /// Created by: linhpv (09/08/2022)
        public object PagingService(int pageIndex, int pageSize, FilterObject[] filterObjects)
        {
            // Xử lý số trang đầu vào
            if (string.IsNullOrEmpty(pageIndex.ToString()) || pageIndex <= 0)
            {
                pageIndex = 1;
            }

            // Xử lý số bản ghi trên 1 trang
            if (string.IsNullOrEmpty(pageSize.ToString()) || pageSize <= 0)
            {
                pageSize = 1;
            }

            // Xử lý tìm câu lệnh tìm kiếm
            string where = "";
            for (int i = 0; i < filterObjects.Length; i++)
            {
                FilterObject filterObject = filterObjects[i];
                // Nếu loại đầu vào là chữ
                if (filterObject.InputType == Enum.InputType.Text)
                {
                    // Xử lý kiểu lọc
                    switch (filterObject.FilterType)
                    {
                        // Chứa
                        case Enum.FilterType.Contain:
                            where += filterObject.ColumnName + " LIKE '%" + filterObject.Value + "%' ";
                            break;
                        // Bằng
                        case Enum.FilterType.Equal:
                            where += filterObject.ColumnName + " = '" + filterObject.Value + "' ";
                            break;
                        // Bắt đầu bằng
                        case Enum.FilterType.StartWith:
                            where += filterObject.ColumnName + " LIKE '" + filterObject.Value + "%' ";
                            break;
                        // Kết thúc bằng
                        case Enum.FilterType.EndWith:
                            where += filterObject.ColumnName + " LIKE '%" + filterObject.Value + "' ";
                            break;
                        // Không chứa
                        case Enum.FilterType.NotContain:
                            where += filterObject.ColumnName + " NOT LIKE '%" + filterObject.Value + "%' ";
                            break;
                    }
                } else if (filterObject.InputType == Enum.InputType.Number) {
                    // Nếu loại đầu vào là số
                    switch (filterObject.FilterType)
                    {
                        case Enum.FilterType.Equal:
                            where += filterObject.ColumnName + " = '" + filterObject.Value + "' ";
                            break;
                        case Enum.FilterType.Less:
                            where += filterObject.ColumnName + " < '" + filterObject.Value + "' ";
                            break;
                        case Enum.FilterType.LessOrEqual:
                            where += filterObject.ColumnName + " <= '" + filterObject.Value + "' ";
                            break;
                        case Enum.FilterType.Greater:
                            where += filterObject.ColumnName + " > '" + filterObject.Value + "' ";
                            break;
                        case Enum.FilterType.GreaterOrEqual:
                            where += filterObject.ColumnName + " >= '" + filterObject.Value + "' ";
                            break;
                        default:
                            break;
                    }
                }
                if (i < filterObjects.Length - 1)
                {
                    where += "AND ";
                }
            }

            // Gọi repository
            var data = _repository.Paging(pageIndex, pageSize, where);

            // Trả về data
            return data;
        }
        #endregion

        #region Override
        /// <summary>
        /// ghi đè phương thức validate
        /// </summary>
        /// <param name="dish">Món ăn</param>
        /// <returns>null - nếu valid, thông báo - nếu không valid</returns>
        protected override string? Validate(Dish dish)
        {
            var langCode = Common.LanguageCode;
            if (string.IsNullOrEmpty(dish.DishName))
            {
                return Resources.Resource.ResourceManager.GetString($"{langCode}_DishName_Empty");
            }
            if (string.IsNullOrEmpty(dish.DishCode))
            {
                return Resources.Resource.ResourceManager.GetString($"{langCode}_DishCode_Empty");
            }
            if (dish.UnitID == Guid.Empty)
            {
                return Resources.Resource.ResourceManager.GetString($"{langCode}_Unit_Empty");
            }
            if (_repository.CheckDuplicateCode(Guid.Empty, dish.DishCode)) {
                return String.Format(Resources.Resource.ResourceManager.GetString($"{langCode}_Duplicate_DishCode"), dish.DishCode);
            }
            return null;
        }
        #endregion

        #region Function
        /// <summary>
        /// Xử lý các chữ có dấu
        /// </summary>
        /// <param name="text">Đầu vào</param>
        /// <returns>Dữ liệu sau khi sử lý</returns>
        /// Created by: linhpv (12/08/2022)
        public string RemoveVietnameseTone(string text)
        {
            string result = text.ToLower();
            result = Regex.Replace(result, "à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g", "a");
            result = Regex.Replace(result, "è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g", "e");
            result = Regex.Replace(result, "ì|í|ị|ỉ|ĩ|/g", "i");
            result = Regex.Replace(result, "ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g", "o");
            result = Regex.Replace(result, "ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g", "u");
            result = Regex.Replace(result, "ỳ|ý|ỵ|ỷ|ỹ|/g", "y");
            result = Regex.Replace(result, "đ", "d");
            return result;
        }
        #endregion
    }
}
