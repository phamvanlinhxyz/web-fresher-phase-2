using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;

namespace MISA.CUKCUK.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DishMaterialController : BaseController<DishMaterial>
    {
        public DishMaterialController(IDishMaterialRepository repository, IDishMaterialService service) : base(repository, service)
        {
        }
    }
}
