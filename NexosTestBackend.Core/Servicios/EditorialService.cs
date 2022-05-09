using NexosTestBackend.Core.Exceptions;
using NexosTestBackend.Core.Interfaces;
using NexosTestBackend.Data.Entidades;
using NexosTestBackend.Data.Interfaces;
using NexosTestBackend.Data.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace NexosTestBackend.Core.Servicios
{
    public class EditorialService : IService<Editorial>
    {
        private IRepository<Editorial> _Repository;

        public EditorialService(string conexion)
        {
            _Repository = new EditorialRepository(conexion);
        }
        public IEnumerable<Editorial> GetAll()
        {
            try
            {
                return _Repository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Editorial Get(int id)
        {
            try
            {
                var editorial = _Repository.GetById(id);
                if (editorial == null)
                    throw new BusinessException("Editorial no existe.");

                return editorial;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert(Editorial editorial)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    if (editorial.MaxLibrosReg == 0)
                        editorial.MaxLibrosReg = -1;

                    _Repository.Insert(editorial);
                    _Repository.Save();

                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Editorial editorial)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var _editorial = _Repository.GetById(editorial.Id);
                    if (_editorial == null)
                        throw new BusinessException("Editorial no existe.");

                    _Repository.Update(editorial);
                    _Repository.Save();

                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var _editorial = _Repository.GetById(id);
                    if (_editorial == null)
                        throw new BusinessException("Editorial no existe.");

                    _Repository.Delete(id);
                    _Repository.Save();

                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
