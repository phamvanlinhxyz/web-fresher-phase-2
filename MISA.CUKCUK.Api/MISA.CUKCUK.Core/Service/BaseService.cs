using MISA.CUKCUK.Core.Interfaces.Repositories;
using MISA.CUKCUK.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Service
{
    /// <summary>
    /// Lớp service cha
    /// </summary>
    /// <typeparam name="T">Tên bảng trong db</typeparam>
    /// Created by: linhpv (08/08/2022)
    public class BaseService<T> : IBaseService<T>
    {
        #region Variable
        IBaseRepository<T> _repository;
        #endregion

        #region Contructor
        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Service
        public int InsertService(T entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateService(T entity)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
