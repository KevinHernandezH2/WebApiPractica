using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class facultadController : ControllerBase
    {

        private readonly equiposContext _facultadContexto;

        public facultadController(equiposContext facultadContexto)
        {
            _facultadContexto = facultadContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<facultad> listadoFacultad = (from m in _facultadContexto.facultades
                                              select m).ToList();

            if (listadoFacultad.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoFacultad);

        }


        [HttpGet]
        [Route("GetbyId/{id}")]
        public IActionResult GetById(int id)
        {

            facultad? facultads = (from m in _facultadContexto.facultades
                                   where m.facultad_id == id
                                   select m).FirstOrDefault();

            if (facultads == null)
            {
                return NotFound();
            }
            return Ok(facultads);
        }


        [HttpGet]
        [Route("find/")]
        public IActionResult GetByName(string filtro)
        {

            List<facultad> listadoFacultad = (from m in _facultadContexto.facultades
                                              where m.nombre_facultad.Contains(filtro)
                                              select m).ToList();

            if (listadoFacultad.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoFacultad);


        }



        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] facultad facultades)
        {
            try
            {
                _facultadContexto.facultades.Add(facultades);
                _facultadContexto.SaveChanges();
                return Ok(facultades);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Actualizar(int id, [FromBody] facultad facultad_Actualizar)
        {


            facultad? facultads = (from m in _facultadContexto.facultades
                                   where m.facultad_id == id
                                   select m).FirstOrDefault();

            if (facultads == null) return NotFound();

            facultads.nombre_facultad = facultad_Actualizar.nombre_facultad;
            facultads.estado = facultad_Actualizar.estado;


            _facultadContexto.Entry(facultads).State = EntityState.Modified;
            _facultadContexto.SaveChanges();
            return Ok(facultad_Actualizar);
        }


        [HttpPut]
        [Route("Eliminar/{id}")]

        public IActionResult Delete(int id)
        {

            facultad? facultads = (from m in _facultadContexto.facultades
                                   where m.facultad_id == id
                                   select m).FirstOrDefault();

            if (facultads == null) return NotFound();

            facultads.estado = "I";

            _facultadContexto.Entry(facultads).State = EntityState.Modified;
            _facultadContexto.SaveChanges();
            return Ok(facultads);

        }
    }
}
