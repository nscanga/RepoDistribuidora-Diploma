using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; }
        public int id_perfil { get; set; }
        public Perfil Perfil { get; set; }
    }
}
