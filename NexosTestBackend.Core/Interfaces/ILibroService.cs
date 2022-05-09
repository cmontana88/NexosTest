using NexosTestBackend.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Core.Interfaces
{
    public interface ILibroService
    {
        LibrosFiltrados ObtenerTodosLosLibrosFiltrados(string fieldFilter, string criterioFilter, int page, int itemxpage);
    }
}
