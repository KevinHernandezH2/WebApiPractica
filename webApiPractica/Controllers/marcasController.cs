using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class marcasController : ControllerBase
    {
        private readonly equiposContext _marcasContexto;

        public marcasController(equiposContext marcasContexto)
        {
            _marcasContexto = marcasContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<marcas> listadoMarcas = (from m in _marcasContexto.marcas
                                          select m).ToList();

            if (listadoMarcas.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoMarcas);

        }


        [HttpGet]
        [Route("GetbyId/{id}")]
        public IActionResult GetById(int id)
        {

            marcas? marcas = (from m in _marcasContexto.marcas
                              where m.id_marcas == id
                              select m).FirstOrDefault();

            if (marcas == null)
            {
                return NotFound();
            }
            return Ok(marcas);
        }


        [HttpGet]
        [Route("find/")]
        public IActionResult GetByName(string filtro)
        {

            List<marcas> listaMarcas = (from m in _marcasContexto.marcas
                                        where m.nombre_marca.Contains(filtro)
                                        select m).ToList();

            if (listaMarcas == null)
            {
                return NotFound();
            }
            return Ok(listaMarcas);
        }


        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] marcas marca)
        {
            try
            {
                _marcasContexto.marcas.Add(marca);
                _marcasContexto.SaveChanges();
                return Ok(marca);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Actualizar(int id, [FromBody] marcas marca_Actualizar)
        {

            marcas? marcas = (from m in _marcasContexto.marcas
                              where m.id_marcas == id
                              select m).FirstOrDefault();

            if (marcas == null) return NotFound();

            marcas.nombre_marca = marca_Actualizar.nombre_marca;
            marcas.estados = marca_Actualizar.estados;

            _marcasContexto.Entry(marcas).State = EntityState.Modified;
            _marcasContexto.SaveChanges();
            return Ok(marca_Actualizar);
        }


        [HttpPut]
        [Route("Eliminar/{id}")]

        public IActionResult Delete(int id)
        {
            marcas? marcas = (from m in _marcasContexto.marcas
                              where m.id_marcas == id
                              select m).FirstOrDefault();

            if (marcas == null) return NotFound();

            marcas.estados = "I";

            _marcasContexto.Entry(marcas).State = EntityState.Modified;
            _marcasContexto.SaveChanges();
            return Ok(marcas);

        }
    }
}
