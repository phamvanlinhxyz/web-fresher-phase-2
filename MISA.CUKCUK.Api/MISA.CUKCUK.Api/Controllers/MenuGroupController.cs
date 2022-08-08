﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;

namespace MISA.CUKCUK.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MenuGroupController : BaseController<MenuGroup>
    {
        public MenuGroupController(IMenuGroupRepository repository, IMenuGroupService service) : base(repository, service)
        {

        }
    }
}
