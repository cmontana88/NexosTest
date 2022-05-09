using NexosTestBackend.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Data.Repositorios
{
    public class EditorialRepository : BaseRepository<Editorial>
    {
        public EditorialRepository(string conexion) : base(conexion)
        {

        }
    }
}
