using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Data.Entidades
{
    [Table("Autor")]
    public partial class Autor : BaseEntity
    {
        public Autor()
        {
            Libro = new HashSet<Libro>();
        }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(100)]
        public string CiudadNacimiento { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        public virtual ICollection<Libro> Libro { get; set; }
    }
}
