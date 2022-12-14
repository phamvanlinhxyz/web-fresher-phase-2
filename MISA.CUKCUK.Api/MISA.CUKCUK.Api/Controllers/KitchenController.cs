using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;

namespace MISA.CUKCUK.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class KitchenController : BaseController<Kitchen>
    {
        public KitchenController(IKitchenRepository repository, IKitchenService service) : base(repository, service)
        {
        }
    }
}
