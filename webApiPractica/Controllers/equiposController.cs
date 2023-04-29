using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiPractica.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting.Server;
using System.Diagnostics.Metrics;
using static webApiPractica.Models.equipo;

namespace webApiPractica.Properties
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _equiposContexto;

        public equiposController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        //Se obtiene toda la informacion de la Base de Datos
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            var listaEquipo = (from e in _equiposContexto.equipos
                               join m in _equiposContexto.marcas on e.marca_id equals m.id_marcas
                               join te in _equiposContexto.tipo_equipo on e.tipo_equipo_id equals te.id_tipo_equipo
                               select new
                               {
                                   e.id_equipos,
                                   e.nombre,
                                   e.descripcion,
                                   e.tipo_equipo_id,
                                   tipo_descripcion = te.descripcion,
                                   e.marca_id,
                                   m.nombre_marca
                               }).ToList();
            if (listaEquipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaEquipo);
        }

        // Se busca en la Base de Datos por ID
        [HttpGet]
        [Route("getbyid/{id}")]

        public IActionResult GetById(int id)
        {
            //El signo ? es porque acepta valores nulos

            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        //Se busca en la Base de Datos por nombre 
        [HttpGet]
        [Route("find/")]

        public IActionResult GetByName(string filtro)
        {

            List<equipos> equipo = (from e in _equiposContexto.equipos
                                    where e.descripcion.Contains(filtro)
                                    select e).ToList();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        // Se usa Post para crear un registro
        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] equipos equipo)
        {
            try
            {
                //Agrega en el contexto
                _equiposContexto.equipos.Add(equipo);
                //Agrega en la base de datos
                _equiposContexto.SaveChanges();
                return Ok(equipo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        // Metodo para modificar los registros en la Base de Datos
        [HttpPut]
        [Route("update/{id}")]

        public IActionResult Actualizar(int id, [FromBody] equipos equipo_Actualizar)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null) return NotFound();

            equipo.nombre = equipo_Actualizar.nombre;
            equipo.descripcion = equipo_Actualizar.descripcion;
            equipo.marca_id = equipo_Actualizar.marca_id;
            equipo.tipo_equipo_id = equipo_Actualizar.tipo_equipo_id;
            equipo.anio_compra = equipo_Actualizar.anio_compra;
            equipo.costo = equipo_Actualizar.costo;

            _equiposContexto.Entry(equipo).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(equipo_Actualizar);
        }

        // Metodo para cambiar el estado registros de la Base de Datos
        [HttpPut]
        [Route("Eliminar/{id}")]

        public IActionResult Delete(int id)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null) return NotFound();

            equipo.estado = "I";

            _equiposContexto.Entry(equipo).State = EntityState.Modified;
            _equiposContexto.SaveChanges();

            return Ok(equipo);



        }
    }

}