using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;

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
        [HttpPost("paging")]
        public IActionResult Paging(int pageIndex, int pageSize, FilterObject[] filterObjects)
        {
            var data = _service.PagingService(pageIndex, pageSize, filterObjects);
            return Ok(data);
        }
        #endregion
    }
}
