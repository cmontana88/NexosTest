using Microsoft.EntityFrameworkCore;
using NexosTestBackend.Data.Entidades;
using NexosTestBackend.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Data.Repositorios
{
    public class LibroRepository : BaseRepository<Libro>, ILibroRepository
    {
        public LibroRepository(string conexion) : base(conexion)
        {

        }

        public IEnumerable<LibroFiltro> GetLibros()
        {
            var libros = _entities.Include(l => l.Autor).Include(l => l.Editorial).AsEnumerable().ToList();

            return GetLibroFiltro(libros);
        }

        public IEnumerable<LibroFiltro> GetLibrosFiltradosporAutor(string criterio)
        {
            var libros = _entities.Include(l => l.Autor).Include(l => l.Editorial).AsEnumerable().Where(l => l.Autor.Nombre.ToLower().Contains(criterio.ToLower())).ToList();
            
            return GetLibroFiltro(libros);
        }

        public IEnumerable<LibroFiltro> GetLibrosFiltradosporAño(string criterio)
        {
            var libros = _entities.Include(l => l.Autor).Include(l => l.Editorial).AsEnumerable().Where(l => l.Año.Equals(Convert.ToInt32(criterio))).ToList();

            return GetLibroFiltro(libros);
        }

        public IEnumerable<LibroFiltro> GetLibrosFiltradosporTitulo(string criterio)
        {
            var libros = _entities.Include(l => l.Autor).Include(l => l.Editorial).AsEnumerable().Where(l => l.Titulo.ToLower().Contains(criterio.ToLower())).ToList();

            return GetLibroFiltro(libros);
        }

        public int GetTotalLibrosByEditorial(int IdEditorial)
        {
            return _entities.Where(lib => lib.IdEditorial.Equals(IdEditorial)).Count();
        }

        private List<LibroFiltro> GetLibroFiltro(List<Libro> libros)
        {
            List<LibroFiltro> librosFiltro = new List<LibroFiltro>();

            foreach (var libro in libros)
            {
                librosFiltro.Add(new LibroFiltro()
                {
                    Id = libro.Id,
                    Titulo = libro.Titulo,
                    Genero = libro.Genero,
                    Ano = libro.Año,
                    NumeroPaginas = libro.NumPaginas,
                    IdAutor = libro.Autor.Id,
                    Autor = libro.Autor.Nombre,
                    IdEditorial = libro.Editorial.Id,
                    Editorial = libro.Editorial.Nombre
                });
            }

            return librosFiltro;
        }
    }
}
