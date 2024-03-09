using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A_2021GB650_2021BS650.Controllers;
using PARCIAL1A_2021GB650_2021BS650.Models;

namespace PARCIAL1A_2021GB650_2021BS650.Controllers
{
    public class autorLibroController : Controller
    {

        private readonly parcial1aContext _parcialContexto;

        public autorLibroController(parcial1aContext parcial1aContexto)
        {
            _parcialContexto = parcial1aContexto;
        }

        ///sumary
        /// EndPoint que retorna el listado de todos los equipos existentes
        /// summary
        /// <return></return>
        [HttpGet]
        [Route("GetAllLibros")]
        public IActionResult Get()
        {
            List<autorlibro> listadoAutorLibro = (from e in _parcialContexto.autorlibro
                                                  select e).ToList();

            if (listadoAutorLibro.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoAutorLibro);
        }


        [HttpPost]
        [Route("Add")]

        public IActionResult GuardarIdAutor([FromBody] autorlibro autorlibros)
        {
            try
            {
                _parcialContexto.autorlibro.Add(autorlibros);
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

        public IActionResult Actualizare(int id, [FromBody] autorlibro autorLibroModificar)
        {
            autorlibro? autorLibroActual = (from e in _parcialContexto.autorlibro
                                     where e.AutorId == id
                                     select e).FirstOrDefault();

            if (autorLibroActual == null)
            {
                return NotFound();
            }

            autorLibroActual.AutorId = autorLibroModificar.AutorId;
            autorLibroActual.LibroId = autorLibroModificar.LibroId;


            _parcialContexto.Entry(autorLibroActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(autorLibroModificar);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarE(int id)
        {
            autorlibro? autoresLibros = (from e in _parcialContexto.autorlibro
                               where e.AutorId == id
                               select e).FirstOrDefault();

            if (autoresLibros == null)
                return NotFound();


            _parcialContexto.autorlibro.Remove(autoresLibros);
            _parcialContexto.SaveChanges();

            return Ok(autoresLibros);
        }
    }
}
