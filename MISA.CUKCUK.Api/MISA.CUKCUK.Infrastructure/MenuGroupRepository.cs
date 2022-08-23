using Microsoft.Extensions.Configuration;
using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web05.Infrastructure
{
    public class MenuGroupRepository : BaseRepository<MenuGroup>, IMenuGroupRepository
    {
        public MenuGroupRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
