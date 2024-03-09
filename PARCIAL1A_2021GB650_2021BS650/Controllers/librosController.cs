using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A_2021GB650_2021BS650.Controllers;
using PARCIAL1A_2021GB650_2021BS650.Models;

namespace PARCIAL1A_2021GB650_2021BS650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class librosController : Controller
    {
        private readonly parcial1aContext _parcialContexto;

        public librosController(parcial1aContext parcial1aContexto)
        {
            _parcialContexto = parcial1aContexto;
        }
        
        ///sumary
        /// EndPoint que retorna el listado de todos los equipos existentes
        /// summary
        /// <return></return>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<libros> listadoLibros = (from e in _parcialContexto.libros
                                           select e).ToList();

            if (listadoLibros.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoLibros);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Guardar([FromBody] libros libro)
        {
            try
            {
                _parcialContexto.libros.Add(libro);
                _parcialContexto.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult Actualizar(int id, [FromBody] libros libroModificar)
        {
            libros? libroActual = (from e in _parcialContexto.libros
                                   where e.Id == id
                                     select e).FirstOrDefault();

            if (libroActual == null)
            {
                return NotFound();
            }

            libroActual.Titulo = libroModificar.Titulo;
         
            _parcialContexto.Entry(libroActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(libroModificar);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            libros? libro = (from e in _parcialContexto.libros
                                where e.Id == id
                               select e).FirstOrDefault();

            if (libro == null)
                return NotFound();

            _parcialContexto.libros.Attach(libro);
            _parcialContexto.libros.Remove(libro);
            _parcialContexto.SaveChanges();

            return Ok(libro);
        }

        // BUSCAR POR NOMBRE
        [HttpGet]
        [Route("GetLibroByNombreAutor/{nombre}")]
        public IActionResult GetByNombre(string nombre)
        {
            var libro = (from l in _parcialContexto.libros
                         join al in _parcialContexto.autorlibro on l.Id equals al.LibroId
                         join a in _parcialContexto.autores on al.AutorId equals a.Id
                         where a.Nombre == nombre
                         select l).FirstOrDefault();

            if (libro == null)
            {
                return NotFound();
            }

            return Ok(libro);
        }
    }
}

