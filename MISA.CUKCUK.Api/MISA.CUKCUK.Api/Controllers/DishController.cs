using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Enum;
using MISA.CUKCUK.Core;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using MISA.CUKCUK.Core.Resources;
using Newtonsoft.Json;

namespace MISA.CUKCUK.Api.Controllers
{
    /// <summary>
    /// Controller xử lý liên quan đến món ăn
    /// </summary>
    /// Created by: linhpv (08/08/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        #region Variable
        IDishRepository _repository;
        IDishService _service;
        #endregion

        #region Contructor
        public DishController(IDishRepository repository, IDishService service) 
        {
            _repository = repository;
            _service = service;
        }
        #endregion

        #region Controller
        /// <summary>
        /// Lấy nguyên vật liệu theo món ăn
        /// </summary>
        /// <param name="dishID">ID món ăn</param>
        /// <returns>Danh sách nguyên vật liệu</returns>
        /// Created by: linhpv (17/08/2022)
        [HttpGet("ListMaterial")]
        public IActionResult GetListMaterial(Guid dishID) 
        {
            try
            {
                // Lấy dữ liệu từ repo
                var data = _repository.GetListMaterial(dishID);

                // Xử lý response
                Response res = new Response(data, true, ErrorCode.NoError, "", "");

                // Trả kết quả cho client
                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa thông tin món ăn 
        /// </summary>
        /// <param name="dish">Món ăn cần sửa</param>
        /// <returns>ID món ăn đã sửa</returns>
        /// Created by: linhpv (17/08/2022)
        [HttpPut]
        public IActionResult Put(Dish dish)
        {
            return Ok(1);
        }

        /// <summary>
        /// Thêm bản ghi mới
        /// </summary>
        /// <param name="dish">Món ăn</param>
        /// <returns>Response</returns>
        /// Created by: linhpv (17/08/2022)
        [HttpPost] 
        public IActionResult Post(Dish dish)
        {
            try
            {
                // Lấy kết quả
                var res = _service.InsertService(dish);

                // Trả lại kết quả cho client
                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy mã mới theo tên
        /// </summary>
        /// <param name="DishName">Tên món ăn</param>
        /// <returns>Trả về mã món ăn</returns>
        /// Created by: linhpv (12/08/2022)
        [HttpGet("NewCode")]
        public IActionResult GetNewCode(string DishName)
        {
            try
            {
                // Lấy dữ liệu và khởi tạo response
                string newCode = _service.AutoGenDishCode(DishName);
                Response res = new Response(newCode, true, ErrorCode.NoError, "", "");

                // Trả về response
                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Xóa bản ghi theo ID
        /// </summary>
        /// <param name="entityID">Id bản ghi</param>
        /// <returns>số bản ghi đã xóa</returns>
        /// Created by: linhpv (11/08/2022)
        [HttpDelete("{entityID}")] 
        public IActionResult Delete(Guid entityID)
        {
            try
            {
                // Lấy dữ liệu và khởi tạo response
                var data = _repository.Delete(entityID);
                Response res = new Response(data, true, ErrorCode.NoError, "", "");

                // Trả về response
                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            
        }

        /// <summary>
        /// Lấy dữ liệu theo phân trang, tìm kiếm
        /// </summary>
        /// <param name="pageIndex">Số trang</param>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="filterObjects">Mảng các điều kiện lọc</param>
        /// <returns>Tổng số bản ghi, tổng số trang, các bản ghi thỏa mãn</returns>
        [HttpPost("Paging")]
        public IActionResult Paging(int pageIndex, int pageSize, FilterObject[] filterObjects)
        {
            try
            {
                // Lấy dữ liệu và khởi tạo response
                var data = _service.PagingService(pageIndex, pageSize, filterObjects);
                Response res = new Response(data: data, success: true, errorCode: ErrorCode.NoError, userMsg: "", devMsg: "");

                // Trả về response
                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion

        #region Function
        private IActionResult HandleException(Exception ex)
        {
            var langCode = Common.LanguageCode;
            Response res = new Response(null, false, ErrorCode.ServerInternal, Resource.ResourceManager.GetString($"{langCode}_Server_Error"), ex.Message);
            return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
        }
        #endregion
    }
}
