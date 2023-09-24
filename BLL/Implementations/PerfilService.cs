using BLL.Interfaces;
using DAL.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Implementations
{
    public class PerfilService : IPerfilService
    {
        private readonly IPerfilRepository _perfilRepository;
        public PerfilService(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        public void Add(Perfil obj)
        {
            throw new NotImplementedException();
        }

        public Perfil FindByTipo(Perfil.TipoPerfil tipo)
        {
            return _perfilRepository.FindByTipo(tipo);
        }

        public List<Perfil> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Perfil obj)
        {
            throw new NotImplementedException();
        }
    }
}
