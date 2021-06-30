using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Carteras.Cartera
{
    public class CarteraViewModel
    {
        [Required]
        public int idcliente { get; set; }
        [Required]
        public string cliente { get; set; }
        [Required]
        public int idusuario { get; set; }
        [Required]
        public string usuario { get; set; }
        [Required]
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
        [Required]
        public string tipo_tasa { get; set; }
        [Required]
        public decimal tasa { get; set; }
        [Required]
        public string capaitalizacion { get; set; }
        [Required]
        public decimal valor_entregado { get; set; }
        [Required]
        public decimal valor_recibido { get; set; }
        [Required]
        public decimal valor_nominal { get; set; }
        [Required]
        public decimal valor_neto { get; set; }
        [Required]
        public decimal TCEA { get; set; }
        [Required]
        public string estado { get; set; }
    }
}
