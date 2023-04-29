using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tipo_EquipoController : ControllerBase
    {

        private readonly equiposContext _tipoEquiposContext;

        public tipo_EquipoController(equiposContext tipoEquiposContext)
        {
            _tipoEquiposContext = tipoEquiposContext;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<tipo_Equipo> tipoEquipoLista = (from t in _tipoEquiposContext.tipo_equipo
                                                 select t).ToList();

            if (tipoEquipoLista == null)
            {
                return NotFound();
            }

            return Ok(tipoEquipoLista);
        }



        [HttpGet]
        [Route("GetbyId/{id}")]

        public IActionResult GetById(int id)
        {

            tipo_Equipo? tipo_Equipo = (from t in _tipoEquiposContext.tipo_equipo
                                        where t.id_tipo_equipo == id
                                        select t).FirstOrDefault();

            if (tipo_Equipo == null)
            {
                return NotFound();
            }
            return Ok(tipo_Equipo);
        }


        [HttpGet]
        [Route("find/")]

        public IActionResult GetByName(string filtro)
        {

            List<tipo_Equipo> listaEquipos = (from m in _tipoEquiposContext.tipo_equipo
                                              where m.descripcion.Contains(filtro)
                                              select m).ToList();

            if (listaEquipos == null)
            {
                return NotFound();
            }
            return Ok(listaEquipos);
        }


        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] tipo_Equipo tipo_Equipo)
        {
            try
            {
                _tipoEquiposContext.tipo_equipo.Add(tipo_Equipo);
                _tipoEquiposContext.SaveChanges();
                return Ok(tipo_Equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Actualizar(int id, [FromBody] tipo_Equipo tipoEquipo_Actualizar)
        {

            tipo_Equipo? tipo_Equipo = (from m in _tipoEquiposContext.tipo_equipo
                                        where m.id_tipo_equipo == id
                                        select m).FirstOrDefault();

            if (tipo_Equipo == null) return NotFound();


            tipo_Equipo.descripcion = tipoEquipo_Actualizar.descripcion;
            tipo_Equipo.estado = tipoEquipo_Actualizar.estado;

            _tipoEquiposContext.Entry(tipo_Equipo).State = EntityState.Modified;
            _tipoEquiposContext.SaveChanges();
            return Ok(tipoEquipo_Actualizar);

        }

        [HttpPut]
        [Route("delete/{id}")]

        public IActionResult Delete(int id)
        {

            tipo_Equipo? tipo_Equipo = (from m in _tipoEquiposContext.tipo_equipo
                                        where m.id_tipo_equipo == id
                                        select m).FirstOrDefault();

            if (tipo_Equipo == null) return NotFound();

            tipo_Equipo.estado = "I";

            _tipoEquiposContext.Entry(tipo_Equipo).State = EntityState.Modified;
            _tipoEquiposContext.SaveChanges();
            return Ok(tipo_Equipo);

        }

    }
}
