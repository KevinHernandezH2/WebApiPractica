using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using webApiPractica.Models;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {

        private readonly equiposContext _usuariosContexto;

        public usuariosController(equiposContext usuariosContexto)
        {
            _usuariosContexto = usuariosContexto;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            var listaUsuarios = (from u in _usuariosContexto.usuarios
                                 join c in _usuariosContexto.carreras on u.carrera_id equals c.carrera_id
                                 select new
                                 {
                                     u.usuario_id,
                                     u.nombre,
                                     u.documento,
                                     u.tipo,
                                     u.carnet,
                                     u.carrera_id,
                                     c.nombre_carrera,
                                     u.estado
                                 }).ToList();
            if (listaUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listaUsuarios);
        }


        [HttpGet]
        [Route("GetbyId/{id}")]
        public IActionResult GetById(int id)
        {

            usuarios? usuario = (from m in _usuariosContexto.usuarios
                                 where m.usuario_id == id
                                 select m).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }


        [HttpGet]
        [Route("find/")]
        public IActionResult GetByName(string filtro)
        {

            List<usuarios> listadoUsuarios = (from m in _usuariosContexto.usuarios
                                              where m.nombre.Contains(filtro)
                                              select m).ToList();

            if (listadoUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoUsuarios);
        }


        [HttpPost]
        [Route("add")]
        public IActionResult Post([FromBody] usuarios usuario)
        {
            try
            {
                _usuariosContexto.usuarios.Add(usuario);
                _usuariosContexto.SaveChanges();
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public IActionResult Actualizar(int id, [FromBody] usuarios usuario_Actualizar)
        {


            usuarios? usuario = (from m in _usuariosContexto.usuarios
                                 where m.usuario_id == id
                                 select m).FirstOrDefault();

            if (usuario == null) return NotFound();

            usuario.nombre = usuario_Actualizar.nombre;
            usuario.documento = usuario_Actualizar.documento;
            usuario.tipo = usuario_Actualizar.tipo;
            usuario.carnet = usuario_Actualizar.carnet;
            usuario.carrera_id = usuario_Actualizar.carrera_id;
            usuario.estado = usuario_Actualizar.estado;

            _usuariosContexto.Entry(usuario).State = EntityState.Modified;
            _usuariosContexto.SaveChanges();
            return Ok(usuario_Actualizar);
        }


        [HttpPut]
        [Route("Eliminar/{id}")]

        public IActionResult Delete(int id)
        {
            usuarios? usuario = (from m in _usuariosContexto.usuarios
                                 where m.usuario_id == id
                                 select m).FirstOrDefault();

            if (usuario == null) return NotFound();

            usuario.estado = "I";

            _usuariosContexto.Entry(usuario).State = EntityState.Modified;
            _usuariosContexto.SaveChanges();
            return Ok(usuario);

        }
    }
}
