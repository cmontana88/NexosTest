using NexosTestBackend.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Data.Interfaces
{
    public interface ILibroRepository 
    {
        int GetTotalLibrosByEditorial(int IdEditorial);
        IEnumerable<LibroFiltro> GetLibrosFiltradosporTitulo(string criterio);
        IEnumerable<LibroFiltro> GetLibrosFiltradosporAutor(string criterio);
        IEnumerable<LibroFiltro> GetLibrosFiltradosporAño(string criterio);
        IEnumerable<LibroFiltro> GetLibros();
    }
}
