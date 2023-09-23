using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPerfilRepository : IGenericRepository<Perfil>
    {
        Perfil GetByTipo(Perfil.TipoPerfil tipo);
    }
}
