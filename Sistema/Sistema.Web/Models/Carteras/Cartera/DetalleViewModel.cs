using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Carteras.Cartera
{
    public class DetalleViewModel
    {
        [Required]
        public int idgasto { get; set; }
        public string gasto { get; set; }
        [Required]
        public decimal valor { get; set; }
        [Required]
        public string tipo_valor { get; set; }
        [Required]
        public string tipo_gasto { get; set; }
    }
}
