using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using Newtonsoft.Json;

namespace MISA.CUKCUK.Api.Controllers
{
    /// <summary>
    /// Controller xử lý liên quan đến món ăn
    /// </summary>
    /// Created by: linhpv (08/08/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishController : BaseController<Dish>
    {
        #region Variable
        IDishRepository _repository;
        IDishService _service;
        #endregion

        #region Contructor
        public DishController(IDishRepository repository, IDishService service) : base(repository, service)
        {
            _repository = repository;
            _service = service;
        }
        #endregion

        #region Controller
        /// <summary>
        /// Lấy mã mới theo tên
        /// </summary>
        /// <param name="DishName">Tên món ăn</param>
        /// <returns>Trả về mã món ăn</returns>
        /// Created by: linhpv (12/08/2022)
        [HttpGet("NewCode")]
        public IActionResult GetNewCode(string DishName)
        {
            // Lấy dữ liệu và khởi tạo response
            string newCode = _service.AutoGenDishCode(DishName);
            Response res = new Response(newCode, true, Core.Enum.ErrorCode.NoError, "", "");

            // Trả về response
            return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
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
                Response res = new Response(data, true, Core.Enum.ErrorCode.NoError, "", "");

                // Trả về response
                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
            }
            catch (Exception ex)
            {
                Response res = new Response(null, false, Core.Enum.ErrorCode.BadRequest, "Có lỗi rồi", ex.Message);
                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
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
            // Lấy dữ liệu và khởi tạo response
            var data = _service.PagingService(pageIndex, pageSize, filterObjects);
            Response res = new Response(data: data, success: true, errorCode: Core.Enum.ErrorCode.NoError, userMsg: "", devMsg: "");

            // Trả về response
            return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
        }
        #endregion
    }
}
