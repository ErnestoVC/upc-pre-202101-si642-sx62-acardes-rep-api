using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Carteras;
using Sistema.Web.Models.Carteras.Cartera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarterasController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public CarterasController(DbContextSistema context)
        {
            _context = context;
        }

        //GET: api/Carteras/Listar
        [Authorize(Roles ="Administrador, Trabajador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<CarteraViewModel>> Listar()
        {
            var cartera = await _context.Carteras
                .Include(v => v.usuarios)
                .Include(v => v.personas)
                .OrderByDescending(v => v.idcartera)
                .Take(100)
                .ToListAsync();

            return cartera.Select(v => new CarteraViewModel
            {
                idcliente = v.idcliente,
                cliente = v.personas.nombre,
                idusuario = v.idusuario,
                usuario = v.usuarios.nombre,
                serie_comprobante = v.serie_comprobante,
                num_comprobante = v.num_comprobante,
                fecha_descuento = v.fecha_descuento,
                fecha_emision = v.fecha_emision,
                fecha_pago = v.fecha_pago,
                moneda = v.moneda,
                tipo_tasa = v.tipo_tasa,
                tasa = v.tasa,
                capaitalizacion = v.capaitalizacion,
                valor_entregado = v.valor_entregado,
                valor_neto = v.valor_neto,
                valor_nominal = v.valor_nominal,
                valor_recibido = v.valor_recibido,
                TCEA = v.valor_recibido,
                estado = v.estado
            });
        }

        //GET: api/Carteras/ListarFultro/texto
        [Authorize(Roles ="Administrador, Trabajador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<CarteraViewModel>> ListarFiltro([FromRoute] string texto)
        {
            var cartera = await _context.Carteras
                .Include(v => v.usuarios)
                .Include(v => v.personas)
                .Where(v => v.num_comprobante.Contains(texto))
                .OrderByDescending(v => v.idcartera)
                .ToListAsync();

            return cartera.Select(v => new CarteraViewModel
            {
                idcliente = v.idcliente,
                cliente = v.personas.nombre,
                idusuario = v.idusuario,
                usuario = v.usuarios.nombre,
                serie_comprobante = v.serie_comprobante,
                num_comprobante = v.num_comprobante,
                fecha_descuento = v.fecha_descuento,
                fecha_emision = v.fecha_emision,
                fecha_pago = v.fecha_pago,
                moneda = v.moneda,
                tipo_tasa = v.tipo_tasa,
                tasa = v.tasa,
                capaitalizacion = v.capaitalizacion,
                valor_entregado = v.valor_entregado,
                valor_neto = v.valor_neto,
                valor_nominal = v.valor_nominal,
                valor_recibido = v.valor_recibido,
                TCEA = v.valor_recibido,
                estado = v.estado
            });            
        }

        //GET: api/Carteras/ListarDetalles
        [Authorize(Roles = "Administrador, Trabajador")]
        [HttpGet("[action]/{idcartera}")]
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalle([FromRoute] int idcartera)
        {
            var detalle = await _context.DetalleCarteras
                .Include(a => a.gastos)
                .Where(d => d.idcartera == idcartera)
                .ToListAsync();

            return detalle.Select(d=> new DetalleViewModel
            {
                idgasto = d.idgasto,
                gasto = d.gastos.nombre,
                valor = d.valor,
                tipo_valor = d.tipo_valor,
                tipo_gasto = d.tipo_gasto
            });
        }

        //POST: api/Carteras/Crear
        [Authorize(Roles ="Administrador, Trabajador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cartera cartera = new Cartera
            {
                idcliente = model.idcliente,
                idusuario = model.idusuario,
                serie_comprobante = model.serie_comprobante,
                num_comprobante = model.num_comprobante,
                fecha_descuento = model.fecha_descuento,
                fecha_emision = model.fecha_emision,
                fecha_pago = model.fecha_pago,
                moneda = model.moneda,
                tipo_tasa = model.tipo_tasa,
                tasa = model.tasa,
                capaitalizacion = model.capaitalizacion,
                valor_entregado = model.valor_entregado,
                valor_neto = model.valor_neto,
                valor_nominal = model.valor_nominal,
                valor_recibido = model.valor_recibido,
                TCEA = model.TCEA,
                estado = model.estado
            };

            try
            {
                _context.Carteras.Add(cartera);
                await _context.SaveChangesAsync();
                var id = cartera.idcartera;
                foreach(var det in model.detalles)
                {
                    DetalleCartera detalle = new DetalleCartera
                    {
                        idcartera = id,
                        idgasto = det.idgasto,
                        valor = det.valor,
                        tipo_gasto = det.tipo_gasto,
                        tipo_valor = det.tipo_valor
                    };
                    _context.DetalleCarteras.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                return BadRequest();
            }
            return Ok();
        }

        //PUT: api/Carteras/Anular/1
        [Authorize(Roles ="Administrador, Trabajador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            var cartera = await _context.Carteras.FirstOrDefaultAsync(v => v.idcartera == id);

            if(cartera == null)
            {
                return NotFound();
            }

            cartera.estado = "Anulado";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        private bool CarteraExiste(int id)
        {
            return _context.Carteras.Any(e => e.idcartera == id);
        }
    }
}
