using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Carteras.Gasto
{
    public class ActualizarViewModel
    {
        [Required]
        public int idgasto { get; set; }
        [Required]
        public string codigo { get; set; }
        [StringLength(50, MinimumLength = 3,
           ErrorMessage = "El nombre no debe de tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [Required]
        public string descripcion { get; set; }
    }
}
