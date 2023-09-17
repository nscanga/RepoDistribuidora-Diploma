using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Notificacion
    {
        public Guid Id { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string Estado { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int CajaId { get; set; }
        public Caja Caja { get; set; }
        public int VentaId { get; set; }
        public Venta Venta { get; set; }
    }

}
