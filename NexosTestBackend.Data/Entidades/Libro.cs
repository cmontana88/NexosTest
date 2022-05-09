using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Data.Entidades
{
    [Table("Libro")]
    public partial class Libro : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        public int Año { get; set; }

        [Required]
        [StringLength(50)]
        public string Genero { get; set; }

        public int NumPaginas { get; set; }

        public int IdEditorial { get; set; }

        public int IdAutor { get; set; }

        public virtual Autor Autor { get; set; }

        public virtual Editorial Editorial { get; set; }
    }
}
