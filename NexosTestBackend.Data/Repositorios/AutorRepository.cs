using NexosTestBackend.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexosTestBackend.Data.Repositorios
{
    public class AutorRepository : BaseRepository<Autor>
    {
        public AutorRepository(string conexion) : base(conexion)
        {

        }
    }
}
