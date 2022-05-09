using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Data.Entidades
{
    [Table("Editorial")]
    public partial class Editorial : BaseEntity
    {
        public Editorial()
        {
            Libro = new HashSet<Libro>();
        }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string DirCorrespondencia { get; set; }

        [Required]
        [StringLength(10)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        public int MaxLibrosReg { get; set; }
        
        public virtual ICollection<Libro> Libro { get; set; }
    }
}
