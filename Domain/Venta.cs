using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Venta
    {
        public Guid Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaVenta { get; set; }
        public int CajaId { get; set; }
        public Caja Caja { get; set; }
    }

}
