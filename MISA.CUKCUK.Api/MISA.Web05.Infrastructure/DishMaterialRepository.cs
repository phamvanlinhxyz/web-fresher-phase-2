﻿using Microsoft.Extensions.Configuration;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructure
{
    public class DishMaterialRepository : BaseRepository<DishMaterial>, IDishMaterialRepository
    {
        public DishMaterialRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
