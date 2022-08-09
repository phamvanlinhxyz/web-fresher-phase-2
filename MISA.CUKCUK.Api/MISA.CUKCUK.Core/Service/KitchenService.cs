using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using MISA.CUKCUK.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Service
{
    public class KitchenService : BaseService<Kitchen>, IKitchenService
    {
        public KitchenService(IKitchenRepository repository) : base(repository)
        {
        }
    }
}
