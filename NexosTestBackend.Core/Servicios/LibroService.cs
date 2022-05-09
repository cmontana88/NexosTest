using Newtonsoft.Json;
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
    public class LibroService : IService<Libro>, ILibroService
    {
        private IRepository<Libro> _Repository;
        private IRepository<Editorial> _RepositoryEditorial;
        private IRepository<Autor> _RepositoryAutor;

        public LibroService(string conexion)
        {
            _Repository = new LibroRepository(conexion);
            _RepositoryEditorial = new EditorialRepository(conexion);
            _RepositoryAutor = new AutorRepository(conexion);
        }
        public IEnumerable<Libro> GetAll()
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

        public Libro Get(int id)
        {
            try
            {
                var _libro = _Repository.GetById(id);
                if (_libro == null)
                    throw new BusinessException("Libro no existe.");

                return _Repository.GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert(Libro libro)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var editorial = _RepositoryEditorial.GetById(libro.IdEditorial);
                    var autor = _RepositoryAutor.GetById(libro.IdAutor);

                    if (editorial == null)
                        throw new BusinessException("La editorial no está registrada.");

                    if (autor == null)
                        throw new BusinessException("El autor no está registrado.");

                    int totalLibros = ((ILibroRepository)_Repository).GetTotalLibrosByEditorial(libro.IdEditorial);

                    if(editorial.MaxLibrosReg != -1)
                        if(totalLibros >= editorial.MaxLibrosReg)
                            throw new BusinessException("No es posible registrar el libro, se alcanzó el máximo permitido.");

                    _Repository.Insert(libro);
                    _Repository.Save();

                    ts.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Libro libro)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var _libro = _Repository.GetById(libro.Id);
                    if (_libro == null)
                        throw new BusinessException("Libro no existe.");

                    var editorial = _RepositoryEditorial.GetById(libro.IdEditorial);
                    var autor = _RepositoryAutor.GetById(libro.IdAutor);

                    if (editorial == null)
                        throw new BusinessException("La editorial no está registrada.");

                    if (autor == null)
                        throw new BusinessException("El autor no está registrado.");

                    int totalLibros = ((ILibroRepository)_Repository).GetTotalLibrosByEditorial(libro.IdEditorial);

                    if (editorial.MaxLibrosReg != -1)
                        if (totalLibros >= editorial.MaxLibrosReg)
                            throw new BusinessException("No es posible registrar el libro, se alcanzó el máximo permitido.");

                    _Repository.Update(libro);
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
                    var _libro = _Repository.GetById(id);
                    if (_libro == null)
                        throw new BusinessException("Libro no existe.");

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

        public LibrosFiltrados ObtenerTodosLosLibrosFiltrados(string fieldFilter, string criterioFilter, int page, int itemxpage)
        {
            try
            {
                IEnumerable<LibroFiltro> libros;

                switch (fieldFilter)
                {
                    case "Autor":
                        libros = ((ILibroRepository)_Repository).GetLibrosFiltradosporAutor(criterioFilter);
                        break;
                    case "Titulo":
                        libros = ((ILibroRepository)_Repository).GetLibrosFiltradosporTitulo(criterioFilter);
                        break;
                    case "Año":
                        libros = ((ILibroRepository)_Repository).GetLibrosFiltradosporAño(criterioFilter);
                        break;
                    default:
                        libros = ((ILibroRepository)_Repository).GetLibros();
                        break;
                }

                var librosFiltrados = new LibrosFiltrados();

                librosFiltrados.Libros = JsonConvert.DeserializeObject<IEnumerable<LibrosFiltradosDto>>(JsonConvert.SerializeObject(libros.ToList().Skip(itemxpage * (page - 1)).Take(itemxpage)));
                librosFiltrados.TotalPages = (libros.Count() / itemxpage) + (libros.Count() % itemxpage == 0 ? 0 : 1);
                librosFiltrados.Pages = librosFiltrados.TotalPages < page ? librosFiltrados.TotalPages : page;

                return librosFiltrados;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
