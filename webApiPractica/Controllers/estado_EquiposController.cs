using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estado_EquiposController : Controller
    {


            private readonly equiposContext _estadosEquipoContexto;

            public estado_EquiposController(equiposContext estadosEquiposContexto)
            {
                _estadosEquipoContexto = estadosEquiposContexto;
            }


            [HttpGet]
            [Route("GetAll")]
            public IActionResult Get()
            {
                List<estados_Equipo> listadoEstado = (from e in _estadosEquipoContexto.estados_equipo
                                                      select e).ToList();

                if (listadoEstado.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoEstado);

            }


            [HttpGet]
            [Route("GetbyId/{id}")]
            public IActionResult GetById(int id)
            {

                estados_Equipo? estados_Equipo = (from m in _estadosEquipoContexto.estados_equipo
                                                  where m.id_estados_equipo == id
                                                  select m).FirstOrDefault();

                if (estados_Equipo == null)
                {
                    return NotFound();
                }
                return Ok(estados_Equipo);
            }


            [HttpGet]
            [Route("find/")]

            public IActionResult GetByName(string filtro)
            {

                List<estados_Equipo> listaEstados = (from m in _estadosEquipoContexto.estados_equipo
                                                     where m.descripcion.Contains(filtro)
                                                     select m).ToList();

                if (listaEstados == null)
                {
                    return NotFound();
                }
                return Ok(listaEstados);
            }


            [HttpPost]
            [Route("Add")]
            public IActionResult Post([FromBody] estados_Equipo estados_Equipo)
            {
                try
                {
                    _estadosEquipoContexto.estados_equipo.Add(estados_Equipo);
                    _estadosEquipoContexto.SaveChanges();
                    return Ok(estados_Equipo);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


            [HttpPut]
            [Route("update/{id}")]
            public IActionResult Actualizar(int id, [FromBody] estados_Equipo estadoActualizar)
            {
                estados_Equipo? estados_Equipo = (from m in _estadosEquipoContexto.estados_equipo
                                                  where m.id_estados_equipo == id
                                                  select m).FirstOrDefault();

                if (estados_Equipo == null) return NotFound();

                estados_Equipo.descripcion = estadoActualizar.descripcion;
                estados_Equipo.estado = estadoActualizar.estado;


                _estadosEquipoContexto.Entry(estados_Equipo).State = EntityState.Modified;
                _estadosEquipoContexto.SaveChanges();
                return Ok(estadoActualizar);
            }


            [HttpPut]
            [Route("Eliminar/{id}")]
            public IActionResult Delete(int id)
            {
                estados_Equipo? estados_Equipo = (from m in _estadosEquipoContexto.estados_equipo
                                                  where m.id_estados_equipo == id
                                                  select m).FirstOrDefault();

                if (estados_Equipo == null) return NotFound();

                estados_Equipo.estado = "I";

                _estadosEquipoContexto.Entry(estados_Equipo).State = EntityState.Modified;
                _estadosEquipoContexto.SaveChanges();
                return Ok(estados_Equipo);

            }
        }
    }

