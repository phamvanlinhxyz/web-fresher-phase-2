using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core;
using MISA.CUKCUK.Core.Enum;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using MISA.CUKCUK.Core.Resources;
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
        /// <summary>
        /// Thêm bản ghi mới
        /// </summary>
        /// <param name="entity">Bản ghi</param>
        /// <returns>ID bản ghi mới</returns>
        /// Created by: linhpv (17/08/2022)
        [HttpPost]
        public IActionResult Post(T entity)
        {
            try
            {
                // Lấy dữ liệu từ service trả về
                var res = _service.InsertService(entity);
                    
                return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả dữ liệu</returns>
        /// Created by: linhpv (12/08/2022)
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Lấy data
                var data = _repository.Get();

                // Khởi tạo response 
                Response response = new Response(data: data, success: true, errorCode: ErrorCode.NoError, userMsg: "", devMsg: "");

                // Trả về response
                return Ok(JsonConvert.SerializeObject(response, Formatting.Indented));
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
        #endregion

        #region Function
        /// <summary>
        /// Xử lý exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Action Result</returns>
        /// Created by: linhpv (11/08/2022)
        protected IActionResult HandleException(Exception ex) 
        {
            Response res = new Response(null, false, ErrorCode.ServerInternal, "", ex.Message);
            return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
        }
        #endregion
    }
}
