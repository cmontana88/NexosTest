using NexosTestBackend.Core.DTOs;
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
    public class AutorService : IService<Autor>
    {
        private IRepository<Autor> _Repository;

        public AutorService(string conexion)
        {
            _Repository = new AutorRepository(conexion);
        }

        public IEnumerable<Autor> GetAll()
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

        public Autor Get(int id)
        {
            try
            {
                var autor = _Repository.GetById(id);
                if (autor == null)
                    throw new BusinessException("Autor no existe.");

                return autor;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert(Autor autor)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    _Repository.Insert(autor);
                    _Repository.Save();

                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Autor autor)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var _autor = _Repository.GetById(autor.Id);
                    if (_autor == null)
                        throw new BusinessException("Autor no existe.");

                    _Repository.Update(autor);
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
                    var _autor = _Repository.GetById(id);
                    if (_autor == null)
                        throw new BusinessException("Autor no existe."); 

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
