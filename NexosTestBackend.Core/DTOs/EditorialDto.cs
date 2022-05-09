using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Core.DTOs
{
    public class EditorialDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DireccionCorrespondencia { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int? MaximoLibrosPermitidos { get; set; }
    }
}
