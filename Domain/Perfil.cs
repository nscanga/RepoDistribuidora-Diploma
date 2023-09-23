using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Perfil
    {
        public Guid IdPerfil { get; set; }
        public enum TipoPerfil { Administrador, Cliente, Empleado, Proveedor }
        public string Descripcion { get; set; }
        public TipoPerfil PerfilTipo { get; set; } 


    }
}
