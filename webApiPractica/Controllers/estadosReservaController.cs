using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estadosReservaController : ControllerBase
    {
        private readonly equiposContext _estadoReservaContexto;

        public estadosReservaController(equiposContext estadoReservaContexto)
        {
            _estadoReservaContexto = estadoReservaContexto;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<estados_Reserva> listadoReservas = (from m in _estadoReservaContexto.estados_reserva
                                                     select m).ToList();

            if (listadoReservas.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoReservas);

        }


        [HttpGet]
        [Route("GetbyId/{id}")]

        public IActionResult GetById(int id)
        {

            estados_Reserva? estado_Reserva = (from m in _estadoReservaContexto.estados_reserva
                                               where m.estado_res_id == id
                                               select m).FirstOrDefault();

            if (estado_Reserva == null)
            {
                return NotFound();
            }
            return Ok(estado_Reserva);
        }


        [HttpGet]
        [Route("find/")]
        public IActionResult GetByName(string filtro)
        {

            List<estados_Reserva> listadoReservas = (from m in _estadoReservaContexto.estados_reserva
                                                     where m.estado.Contains(filtro)
                                                     select m).ToList();

            if (listadoReservas.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoReservas);
        }


        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] estados_Reserva estado_Res)
        {
            try
            {
                _estadoReservaContexto.estados_reserva.Add(estado_Res);
                _estadoReservaContexto.SaveChanges();
                return Ok(estado_Res);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Actualizar(int id, [FromBody] estados_Reserva reservaActualizar)
        {

            estados_Reserva? estado_Reserva = (from m in _estadoReservaContexto.estados_reserva
                                               where m.estado_res_id == id
                                               select m).FirstOrDefault();

            if (estado_Reserva == null) return NotFound();

            estado_Reserva.estado = reservaActualizar.estado;


            _estadoReservaContexto.Entry(estado_Reserva).State = EntityState.Modified;
            _estadoReservaContexto.SaveChanges();
            return Ok(reservaActualizar);
        }


        [HttpPut]
        [Route("Eliminar/{id}")]

        public IActionResult Delete(int id)
        {
            estados_Reserva? estado_Reserva = (from m in _estadoReservaContexto.estados_reserva
                                               where m.estado_res_id == id
                                               select m).FirstOrDefault();

            if (estado_Reserva == null) return NotFound();

            estado_Reserva.estado = "I";

            _estadoReservaContexto.Entry(estado_Reserva).State = EntityState.Modified;
            _estadoReservaContexto.SaveChanges();
            return Ok(estado_Reserva);

        }
    }
}
