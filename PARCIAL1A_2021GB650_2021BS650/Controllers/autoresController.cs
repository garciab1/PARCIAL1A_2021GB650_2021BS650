using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A_2021GB650_2021BS650.Controllers;
using PARCIAL1A_2021GB650_2021BS650.Models;

namespace PARCIAL1A_2021GB650_2021BS650.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class autoresController : Controller
    {
        private readonly parcial1aContext _parcialContexto;

        public autoresController(parcial1aContext parcialContexto)
        {
            _parcialContexto = parcialContexto;
        }

        ///sumary
        /// EndPoint que retorna el listado de todos los equipos existentes
        /// summary
        /// <return></return>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<autores> listadoautores = (from a in _parcialContexto.autores
                                            select a).ToList();

            if (listadoautores.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoautores);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            autores? autores = (from a in _parcialContexto.autores
                                where a.Id == id
                               select a).FirstOrDefault();
            if (autores == null)
            {
                return NotFound();
            }
            return Ok(autores);
        }

        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            autores? autores = (from a in _parcialContexto.autores
                                where a.Nombre.Contains(filtro)
                               select a).FirstOrDefault();
            if (autores == null)
            {
                return NotFound();
            }
            return Ok(autores);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Guardare([FromBody] autores autore)
        {
            try
            {
                _parcialContexto.autores.Add(autore);
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

        public IActionResult Actualizare(int id, [FromBody] autores autoresModificar)
        {
            autores? autoresActual = (from a in _parcialContexto.autores
                                      where a.Id== id
                                     select a).FirstOrDefault();

            if (autoresActual == null)
            {
                return NotFound();
            }

            autoresActual.Nombre = autoresModificar.Nombre;
          

            _parcialContexto.Entry(autoresActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(autoresModificar);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarE(int id)
        {
            autores? autor = (from a in _parcialContexto.autores
                              where a.Id== id
                               select a).FirstOrDefault();

            if (autor == null)
                return NotFound();

            _parcialContexto.autores.Attach(autor);
            _parcialContexto.autores.Remove(autor);
            _parcialContexto.SaveChanges();

            return Ok(autor);
        }

    }
}
}
