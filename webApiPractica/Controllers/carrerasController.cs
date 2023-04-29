using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;
namespace webApiPractica.Controllers


{
    [Route("api/[controller]")]
    [ApiController]
    public class carrerasController : Controller
    {
            private readonly equiposContext _carreraContexto;

            public carrerasController(equiposContext carrerasContexto)
            {
                _carreraContexto = carrerasContexto;
            }


            [HttpGet]
            [Route("GetAll")]
            public IActionResult Get()
            {
                var listadoCarreras = (from c in _carreraContexto.carreras
                                       join f in _carreraContexto.facultades on c.facultad_id equals f.facultad_id
                                       select new
                                       {
                                           c.carrera_id,
                                           c.nombre_carrera,
                                           c.facultad_id,
                                           f.nombre_facultad,
                                           c.estado
                                       }).ToList();
                if (listadoCarreras.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadoCarreras);
            }


            [HttpGet]
            [Route("GetAllById/{id}")]
            public IActionResult GetById()
            {
                carreras? carreras = (from c in _carreraContexto.carreras
                                      select c).FirstOrDefault();

                if (carreras == null)
                {
                    return NotFound();
                }
                return Ok(carreras);
            }


            [HttpGet]
            [Route("find/")]
            public IActionResult GetByName(string filtro)
            {
                List<carreras> listadocarreras = (from c in _carreraContexto.carreras
                                                  where c.nombre_carrera.Contains(filtro)
                                                  select c).ToList();
                if (listadocarreras.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listadocarreras);
            }


            [HttpPost]
            [Route("Add")]
            public IActionResult Post([FromBody] carreras carrera)
            {
                try
                {

                    _carreraContexto.carreras.Add(carrera);
                    _carreraContexto.SaveChanges();
                    return Ok(carrera);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

            [HttpPut]
            [Route("update/{id}")]
            public IActionResult Actualizar(int id, [FromBody] carreras carrerasModificar)
            {
                carreras? carrera = (from e in _carreraContexto.carreras
                                     where e.carrera_id == id
                                     select e).FirstOrDefault();

                if (carrera == null) return NotFound();

                carrera.nombre_carrera = carrerasModificar.nombre_carrera;
                carrera.facultad_id = carrerasModificar.facultad_id;
                carrera.estado = carrerasModificar.estado;


                _carreraContexto.Entry(carrera).State = EntityState.Modified;
                _carreraContexto.SaveChanges();

                return Ok(carrerasModificar);

            }

            [HttpPut]
            [Route("Eliminar/{id}")]
            public IActionResult Eliminarcarreras(int id)
            {
                carreras? carrera = (from e in _carreraContexto.carreras
                                     where e.carrera_id == id
                                     select e).FirstOrDefault();
                if (carrera == null) return NotFound();

                carrera.estado = "I";
                _carreraContexto.Entry(carrera).State = EntityState.Modified;
                _carreraContexto.SaveChanges();

                return Ok(carrera);

            }
        }
}
