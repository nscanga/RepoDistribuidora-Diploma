using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CompraProveedor
    {
        public Guid Id { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor Proveedor { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCompra { get; set; }
    }

}
