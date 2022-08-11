using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
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
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Lấy data
                var data = JsonConvert.SerializeObject(_repository.Get(), Formatting.Indented);

                // Trả về data
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
