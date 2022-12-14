using Microsoft.AspNetCore.Http;
using MISA.CUKCUK.Core.Enum;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using MISA.CUKCUK.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using Microsoft.Extensions.Configuration;

namespace MISA.CUKCUK.Core.Service
{
    /// <summary>
    /// Dish service
    /// </summary>
    public class DishService : BaseService<Dish>, IDishService
    {
        #region Variable
        IDishRepository _repository;
       IConfiguration _configuration;
        #endregion

        #region Contructor
        /// <summary>
        /// Hàm khởi tạo 
        /// </summary>
        /// <param name="repository">repo</param>
        /// Created by: linhpv (15/08/2022)
        public DishService(IDishRepository repository, IConfiguration configuration) : base (repository)
        {
            _repository = repository;
            _configuration = configuration;
        }
        #endregion

        #region Service
        /// <summary>
        /// Service upload ảnh
        /// </summary>
        /// <param name="image">Ảnh</param>
        /// <returns>response</returns>
        /// Created by: linhpv (24/08/2022)
        public async Task<Response> UploadServiceAsync(IFormFile image)
        {
            // Check dung lượng file ảnh
            if (image.Length > 5 * 1024 * 1024)
            {
                return new Response(null, false, ErrorCode.OverSize, "", "");
            }

            // Khởi tạo file name
            var extention = ".jpg";
            string fileName = "Dish-" + DateTime.Now.Ticks + extention;

            // Tạo đường dẫn
            var pathBuild = Path.Combine(Directory.GetCurrentDirectory(), _configuration["StaticFolder:UploadPath"]);

            if (!Directory.Exists(pathBuild))
            {
                Directory.CreateDirectory(pathBuild);
            }

            // Khởi tạo đường dẫn ảnh
            var path = Path.Combine(Directory.GetCurrentDirectory(), _configuration["StaticFolder:UploadPath"], fileName);

            // Copy ảnh theo đường dẫn
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Tạo link ảnh trả về cho client
            var linkImage = _configuration["StaticFolder:UploadLink"] + fileName;

            return new Response(linkImage, true, ErrorCode.NoError, "", "");
        }

        /// <summary>
        /// Service sửa thông tin món ăn
        /// </summary>
        /// <param name="dish">Món ăn cần sửa</param>
        /// <returns>responst</returns>
        /// Created by: linhpv (18/08/2022)
        public Response UpdateService(Dish dish)
        {
            // Validate dữ liệu
            ErrorCode error = Validate(dish, dish.DishID);

            if (error == ErrorCode.NoError)
            {
                // Nếu không có nguyên vật liệu thì set định lượng nguyên vật liệu = 0 nếu đã định lượng set = 1
                if (dish.DishMaterials == null || dish.DishMaterials.Count == 0)
                {
                    dish.MaterialQuantified = Enum.MaterialQuantified.NotQuantified;
                }
                else
                {
                    dish.MaterialQuantified = Enum.MaterialQuantified.Quantified;
                }
                // Gọi repo lấy kết quả
                var newID = _repository.Update(dish);
                // Check có ID trả về hay không
                if (newID != Guid.Empty)
                {
                    return new Response(newID, true, error, "", "");
                }
                error = ErrorCode.EditFailed;
            }
            return new Response(null, false, error, "", "");
        }

        /// <summary>
        /// Thêm món ăn mới
        /// </summary>
        /// <param name="dish">Món ăn mới</param>
        /// <param name="dishMaterials">Danh sách nguyên vật liệu</param>
        /// <returns>Trả về ID mới</returns>
        /// Created by: linhpv (17/08/2022)
        public override Response InsertService(Dish dish)
        {
            // Validate dữ liệu
            ErrorCode error = Validate(dish, Guid.Empty);

            // Kiểm tra có error gì không
            if (error == ErrorCode.NoError)
            {
                // Nếu không có nguyên vật liệu thì set định lượng nguyên vật liệu = 0 nếu đã định lượng set = 1
                if (dish.DishMaterials == null || dish.DishMaterials.Count == 0)
                {
                    dish.MaterialQuantified = Enum.MaterialQuantified.NotQuantified;
                } 
                else
                {
                    dish.MaterialQuantified = Enum.MaterialQuantified.Quantified;
                }
                // Gọi repo lấy kết quả
                var newID = _repository.Insert(dish);
                // Check có ID trả về hay không
                if (newID != Guid.Empty)
                {
                    return new Response(data: newID, success: true, error, userMsg: "", devMsg: "");
                }
                error = ErrorCode.AddFailed;
            }
            return new Response(data: null, success: false, error, userMsg: "", devMsg: "");
        }

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
            if (_repository.CheckDuplicate(Guid.Empty, newCode, "DishCode"))
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

                if (_repository.CheckDuplicate(Guid.Empty, newCode, "DishCode"))
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
        public object PagingService(int pageIndex, int pageSize, FilterObject[] filterObjects, string? sortBy, string? sortType)
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

            // Lấy câu where
            string where = CreateWhereString(filterObjects);
            // Lấy câu sort
            string? sort = CreateSortString(sortBy, sortType);

            // Gọi repository
            var data = _repository.Paging(pageIndex, pageSize, where, sort);
            // Trả về data
            return data;
        }
        #endregion

        #region Function
        /// <summary>
        /// Xử lý sinh câu sort
        /// </summary>
        /// <param name="sortBy">Sort theo trường nào</param>
        /// <param name="sortType">Kiểu sort là gì</param>
        /// <returns>Câu lệnh sort</returns>
        /// Created by: linhpv (24/08/2022)
        private string? CreateSortString (string? sortBy, string? sortType)
        {
            // Kiểm tra xem có sort by hay không
            if (string.IsNullOrEmpty(sortBy))
            {
                return null; 
            }

            // Tạo câu sort
            string sort = sortBy + " ";

            // Kiểm tra xe có kiểu sort hay không
            if (string.IsNullOrEmpty(sortType)) {
                sort += "ASC";
            }  
            else
            {
                sort += sortType;
            }

            return sort;
        }

        /// <summary>
        /// Xử lý sinh câu điều kiện where
        /// </summary>
        /// <param name="filterObjects">Mảng lọc truyền vào</param>
        /// <returns>Câu lệnh where hoàn chỉnh</returns>
        /// Created by: linhpv (24/08/2022)
        private string CreateWhereString(FilterObject[] filterObjects)
        {
            // Xử lý tìm câu lệnh tìm kiếm
            string where = "";
            for (int i = 0; i < filterObjects.Length; i++)
            {

                FilterObject filterObject = filterObjects[i];
                // Check giá trị xem có trống hay không => nếu có thì bỏ qua
                if (string.IsNullOrEmpty(filterObject.Value) && filterObject.InputType != Enum.InputType.Boolean)
                {
                    continue;
                }

                // Check nếu không phải phần tử đầu thì thêm AND
                if (!string.IsNullOrEmpty(where))
                {
                    where += "AND ";
                }

                // Nếu loại đầu vào là chữ
                if (filterObject.InputType == InputType.Text)
                {
                    // Xử lý kiểu lọc
                    switch (filterObject.FilterType)
                    {
                        // Chứa
                        case FilterType.Contain:
                            where += filterObject.ColumnName + " LIKE '%" + filterObject.Value + "%' ";
                            break;
                        // Bằng
                        case FilterType.Equal:
                            where += filterObject.ColumnName + " = '" + filterObject.Value + "' ";
                            break;
                        // Bắt đầu bằng
                        case FilterType.StartWith:
                            where += filterObject.ColumnName + " LIKE '" + filterObject.Value + "%' ";
                            break;
                        // Kết thúc bằng
                        case FilterType.EndWith:
                            where += filterObject.ColumnName + " LIKE '%" + filterObject.Value + "' ";
                            break;
                        // Không chứa
                        case FilterType.NotContain:
                            where += filterObject.ColumnName + " NOT LIKE '%" + filterObject.Value + "%' ";
                            break;
                        default:
                            break;
                    }
                }
                else if (filterObject.InputType == InputType.Number)
                {
                    // Nếu loại đầu vào là số
                    switch (filterObject.FilterType)
                    {
                        // Bằng
                        case FilterType.Equal:
                            where += filterObject.ColumnName + " = '" + filterObject.Value + "' ";
                            break;
                        // Nhỏ hơn
                        case FilterType.Less:
                            where += filterObject.ColumnName + " < '" + filterObject.Value + "' ";
                            break;
                        // Nhỏ hơn hoặc bằng
                        case FilterType.LessOrEqual:
                            where += filterObject.ColumnName + " <= '" + filterObject.Value + "' ";
                            break;
                        // Lớn hơn
                        case FilterType.Greater:
                            where += filterObject.ColumnName + " > '" + filterObject.Value + "' ";
                            break;
                        // Lớn hơn hoặc bằng
                        case FilterType.GreaterOrEqual:
                            where += filterObject.ColumnName + " >= '" + filterObject.Value + "' ";
                            break;
                        default:
                            break;
                    }
                }
                else if (filterObject.InputType == InputType.Boolean)
                {
                    // Nếu đầu vào dạng bool
                    switch (filterObject.FilterType)
                    {
                        // Đúng
                        case FilterType.True:
                            where += filterObject.ColumnName + " = '1' ";
                            break;
                        // Sai
                        case FilterType.False:
                            where += filterObject.ColumnName + " = '0' ";
                            break;
                        default:
                            break;
                    }
                }
            }

            return where;
        }

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="dish">Món ăn</param>
        /// <param name="dishID">ID món ăn</param>
        /// <returns>null - nếu valid, thông báo - nếu không valid</returns>
        /// Created by: linhpv (18/08/2022)
        private ErrorCode Validate(Dish dish, Guid? dishID)
        {
            // Check tên món ăn trống
            if (string.IsNullOrEmpty(dish.DishName))
            {
                return ErrorCode.EmptyName;
            }
            // Check mã món ăn trống
            if (string.IsNullOrEmpty(dish.DishCode))
            {
                return ErrorCode.EmptyCode;
            }
            // Check đơn vị tính trống
            if (dish.UnitID == Guid.Empty)
            {
                return ErrorCode.EmptyUnit;
            }
            // Check giá bán trống
            if (!dish.Price.HasValue)
            {
                return ErrorCode.EmptyPrice;
            }
            // Check trùng mã món ăn
            if (_repository.CheckDuplicate(dishID, dish.DishCode, "DishCode"))
            {
                return ErrorCode.DuplicateCode;
            }
            return ErrorCode.NoError;
        }

        /// <summary>
        /// Xử lý các chữ có dấu
        /// </summary>
        /// <param name="text">Đầu vào</param>
        /// <returns>Dữ liệu sau khi sử lý</returns>
        /// Created by: linhpv (12/08/2022)
        private string RemoveVietnameseTone(string text)
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
