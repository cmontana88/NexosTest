using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Core.DTOs
{
    public class LibrosFiltrados
    {
        public int TotalPages { get; set; }
        public int Pages { get; set; }

        public IEnumerable<LibrosFiltradosDto> Libros { get; set; }
    }
}
