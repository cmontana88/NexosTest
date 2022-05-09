using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Core.DTOs
{
    public class LibrosFiltradosDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string Genero { get; set; }
        public int NumeroPaginas { get; set; }
        public int IdEditorial { get; set; }
        public int IdAutor { get; set; }
        public string Editorial { get; set; }
        public string Autor { get; set; }
    }
}
