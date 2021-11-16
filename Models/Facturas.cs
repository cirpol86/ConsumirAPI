using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumirAPI.Models
{
    internal class Facturas
    {
        public int IdFactura { get; set; }
        public int NumeroFactura { get; set; }
        public string TipoPago { get; set; }
        public int DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public float SubTotal { get; set; }
        public int Descuento { get; set; }
        public int IVA { get; set; }
        public int TotalDescuento { get; set; }
        public int TotalImpuesto { get; set; }
        public float Total { get; set; }
    }
}
