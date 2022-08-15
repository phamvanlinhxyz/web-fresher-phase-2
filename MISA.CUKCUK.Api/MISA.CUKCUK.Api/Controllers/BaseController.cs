using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Enum;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using Newtonsoft.Json;

namespace MISA.CUKCUK.Api.Controllers
{
    /// <summary>
    /// Lớp controller cơ sở
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: linhpv (07/08/2022)
    [Route("api/v1/'[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        #region Variable
        IBaseRepository<T> _repository;
        IBaseService<T> _service;
        #endregion

        #region Contructor
        public BaseController(IBaseRepository<T> repository, IBaseService<T> service)
        {
            _repository = repository;
            _service = service;
        }
        #endregion

        #region Controller
        [HttpPost]
        public IActionResult Post(T entity)
        {
            var res = _service.InsertService(entity);
            return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả dữ liệu</returns>
        /// Created by: linhpv (12/08/2022)
        [HttpGet]
        public IActionResult Get()
        {
            // Lấy data
            var data = _repository.Get();

            // Khởi tạo response 
            Response response = new Response(data: data, success: true, errorCode: ErrorCode.NoError, userMsg: "", devMsg: "");

            // Trả về response
            return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
        }
        #endregion
    }
}
