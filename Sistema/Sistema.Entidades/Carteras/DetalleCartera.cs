using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Carteras
{
    public class DetalleCartera
    {
        public int iddetalle_cartera { get; set; }
        [Required]
        public int idcartera { get; set; }
        [Required]
        public int idgasto { get; set; }
        [Required]
        public decimal valor { get; set; }
        [Required]
        public string tipo_valor { get; set; }
        [Required]
        public string tipo_gasto { get; set; }

        public Cartera carteras { get; set; }
        public Gasto gastos { get; set; }
    }
}
