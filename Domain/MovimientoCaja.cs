using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MovimientoCaja
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CajaId { get; set; }
        public Caja Caja { get; set; }
        public int VentaId { get; set; }
        public Venta Venta { get; set; }
    }

}
