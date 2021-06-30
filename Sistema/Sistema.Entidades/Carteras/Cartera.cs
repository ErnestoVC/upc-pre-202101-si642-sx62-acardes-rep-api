using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Carteras
{
    public class Cartera
    {
        public int idcartera { get; set; }
        [Required]
        public int idcliente { get; set; }
        [Required]
        public int idusuario { get; set; }
        public string serie_comprobante { get; set; }
        [Required]
        public string num_comprobante { get; set; }
        [Required]
        public DateTime fecha_emision { get; set; }
        [Required]
        public DateTime fecha_pago { get; set; }
        [Required]
        public DateTime fecha_descuento { get; set; }
        [Required]
        public string moneda { get; set; }
        public string tipo_tasa { get; set; }
        public decimal tasa { get; set; }
        public string capaitalizacion { get; set; }
        public decimal valor_entregado { get; set; }
        public decimal valor_recibido { get; set; }
        public decimal valor_nominal { get; set; }
        public decimal valor_neto { get; set; }
        public decimal TCEA { get; set; }
        public string estado { get; set; }

        public ICollection<DetalleCartera> detalles { get; set; }
        public Usuario usuarios { get; set; }
        public Persona personas { get; set; }
    }
}
