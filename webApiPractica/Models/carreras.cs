using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace webApiPractica.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class reservasController : ControllerBase
    {

        private readonly equiposContext _reservasContexto;

        public reservasController(equiposContext reservasContexto)
        {
            _reservasContexto = reservasContexto;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var listadoReservas = (from e in _reservasContexto.reservas
                                   join eq in _reservasContexto.equipos on e.equipo_id equals eq.id_equipos
                                   join u in _reservasContexto.usuarios on e.usuario_id equals u.usuario_id
                                   join re in _reservasContexto.estados_reserva on e.estado_reserva_id equals re.estado_res_id
                                   select new
                                   {
                                       e.reserva_id,
                                       e.equipo_id,
                                       eq.nombre,
                                       eq.descripcion,
                                       eq.costo,
                                       e.usuario_id,
                                       nombreuser = u.nombre,
                                       u.documento,
                                       u.carnet,
                                       e.fecha_salida,
                                       e.fecha_retorno,
                                       e.tiempo_reserva,
                                       e.estado_reserva_id,
                                       estado_reserva = re.estado,
                                       e.estado
                                   }).ToList();
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

            reservas? reserva = (from m in _reservasContexto.reservas
                                 where m.reserva_id == id
                                 select m).FirstOrDefault();

            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }


        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] reservas reserva)
        {
            try
            {
                _reservasContexto.reservas.Add(reserva);
                _reservasContexto.SaveChanges();
                return Ok(reserva);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Actualizar(int id, [FromBody] reservas reserva_Actualizar)
        {

            reservas? reserva = (from m in _reservasContexto.reservas
                                 where m.reserva_id == id
                                 select m).FirstOrDefault();

            if (reserva == null) return NotFound();

            reserva.equipo_id = reserva_Actualizar.equipo_id;
            reserva.usuario_id = reserva_Actualizar.usuario_id;
            reserva.fecha_salida = reserva_Actualizar.fecha_salida;
            reserva.hora_salida = reserva_Actualizar.hora_salida;
            reserva.tiempo_reserva = reserva_Actualizar.tiempo_reserva;
            reserva.estado_reserva_id = reserva_Actualizar.estado_reserva_id;
            reserva.fecha_retorno = reserva_Actualizar.fecha_retorno;
            reserva.hora_retorno = reserva_Actualizar.hora_retorno;
            reserva.estado = reserva_Actualizar.estado;

            _reservasContexto.Entry(reserva).State = EntityState.Modified;
            _reservasContexto.SaveChanges();
            return Ok(reserva_Actualizar);
        }


        [HttpPut]
        [Route("Eliminar/{id}")]
        public IActionResult Delete(int id)
        {
            reservas? reserva = (from m in _reservasContexto.reservas
                                 where m.reserva_id == id
                                 select m).FirstOrDefault();

            if (reserva == null) return NotFound();

            reserva.estado = "I";

            _reservasContexto.Entry(reserva).State = EntityState.Modified;
            _reservasContexto.SaveChanges();
            return Ok(reserva);

        }

    }
}
