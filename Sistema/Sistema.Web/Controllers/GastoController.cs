using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Carteras;
using Sistema.Web.Models.Carteras.Gasto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Authorize(Roles = "Administrador,Trabajador")]
    [Route("api/[Controller]")]
    [ApiController]
    public class GastoController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public GastoController(DbContextSistema context)
        {
            _context = context;
        }

        //GET: api/Gasto/Listar
        [HttpGet("[action]")]
        public async Task <IEnumerable<GastoViewModel>> Listar()
        {
            var gasto = await _context.Gastos.ToListAsync();

            return gasto.Select(c => new GastoViewModel
            {
                idgasto = c.idgasto,
                codigo = c.codigo,
                nombre = c.nombre,
                descripcion = c.descripcion,
                condicion = c.condicion
            });
        }

        //GET: api/Gasto/Listar
        [Authorize(Roles = "Administrador,Trabajador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<GastoViewModel>> ListarGasto([FromRoute] string texto)
        {
            var gasto = await _context.Gastos.Where(a => a.nombre.Contains(texto))
                .Where(a => a.condicion == true)
                .ToListAsync();

            return gasto.Select(a => new GastoViewModel
            {
                idgasto = a.idgasto,
                codigo = a.codigo,
                nombre = a.nombre,
                descripcion = a.descripcion,
                condicion = a.condicion
            });
        }

        //GET: api/Gasto/Mostar/1
        [Authorize(Roles = "Administrador,Trabajador")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var gasto = await _context.Gastos.SingleOrDefaultAsync(a => a.idgasto == id);

            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(new GastoViewModel
            {
                idgasto = gasto.idgasto,
                codigo = gasto.codigo,
                nombre = gasto.nombre,
                descripcion = gasto.descripcion,
                condicion = gasto.condicion
            });
        }

        //GET: api/Gasto/BuscarCodigoGasto/12345678
        [Authorize(Roles = "Administrador,Trabajador")]
        [HttpGet("[action]/{codigo}")]
        public async Task<IActionResult> BuscarCodigoGasto ([FromRoute] string codigo)
        {
            var gasto = await _context.Gastos.Where(a => a.condicion == true)
                .SingleOrDefaultAsync(a => a.codigo == codigo);

            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(new GastoViewModel
            {
                idgasto = gasto.idgasto,
                codigo = gasto.codigo,
                nombre = gasto.nombre,
                descripcion = gasto.descripcion,
                condicion = gasto.condicion
            });
        }

        //PUT: api/Gasto/Actualizar
        [Authorize(Roles = "Administrador,Trabajador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(model.idgasto <= 0)
            {
                return BadRequest();
            }

            var gasto = await _context.Gastos.FirstOrDefaultAsync(a => a.idgasto == model.idgasto);

            if(gasto == null)
            {
                return NotFound();
            }

            gasto.idgasto = model.idgasto;
            gasto.codigo = model.codigo;
            gasto.nombre = model.nombre;
            gasto.descripcion = model.descripcion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize(Roles ="Administrador, Trabajador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Gasto gasto = new Gasto
            {
                idgasto = model.idgasto,
                codigo = model.codigo,
                nombre = model.nombre,
                descripcion = model.descripcion,
                condicion = true
            };

            _context.Gastos.Add(gasto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize(Roles ="Administrador,Trabajador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar ([FromRoute] int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            var gasto = await _context.Gastos.FirstOrDefaultAsync(a => a.idgasto == id);

            if(gasto == null)
            {
                return NotFound();
            }

            gasto.condicion = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize(Roles = "Administrador,Trabajador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var gasto = await _context.Gastos.FirstOrDefaultAsync(a => a.idgasto == id);

            if (gasto == null)
            {
                return NotFound();
            }

            gasto.condicion = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        private bool GastoExiste(int id)
        {
            return _context.Gastos.Any(e => e.idgasto == id);
        }
    }
}
