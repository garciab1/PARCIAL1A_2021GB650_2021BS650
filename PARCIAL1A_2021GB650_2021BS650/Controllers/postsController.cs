using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PARCIAL1A_2021GB650_2021BS650.Controllers;
using PARCIAL1A_2021GB650_2021BS650.Models;

namespace PARCIAL1A_2021GB650_2021BS650.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class postsController : Controller
    {
        private readonly parcial1aContext _parcialContexto;
        public postsController(parcial1aContext parcialContexto)
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
            List<posts> listadoposts = (from p in _parcialContexto.posts
                                            select p).ToList();

            if (listadoposts.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoposts);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            posts? posts = (from p in _parcialContexto.posts
                                where p.Id == id
                                select p).FirstOrDefault();
            if (posts == null)
            {
                return NotFound();
            }
            return Ok(posts);
        }

       


        [HttpGet]
        [Route("Find/{filtro}")]

        public IActionResult FindByDescription(string filtro)
        {
            posts? posts = (from p in _parcialContexto.posts
                            where p.Titulo.Contains(filtro)
                                select p).FirstOrDefault();
            if (posts == null)
            {
                return NotFound();
            }
            return Ok(posts);
        }

        /// por autor  

   

        [HttpGet]
        [Route("GetAllAutores")]
        public IActionResult GetNombres()
        {
            var posts = (from po in _parcialContexto.posts
                         join a in _parcialContexto.autores
                         on po.AutorId equals a.Id

                         select new
                         {
                             po.Id,
                             a.Nombre

                         }).OrderBy(resultado => resultado.Nombre)
                         .Take(20)
                         .ToList();

            if (posts.Count == 0)
            {
                return NotFound();
            }
            return Ok(posts);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Guardare([FromBody] posts post)
        {
            try
            {
                _parcialContexto.posts.Add(post);
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

        public IActionResult Actualizare(int id, [FromBody] posts postsModificar)
        {
            posts? postsActual = (from p in _parcialContexto.posts
                                  where p.Id == id
                                      select p).FirstOrDefault();

            if (postsActual == null)
            {
                return NotFound();
            }

            postsActual.Titulo = postsModificar.Titulo;
            postsActual.Contenido = postsModificar.Contenido;
            postsActual.FechaPublicacion = postsModificar.FechaPublicacion;
            postsActual.AutorId = postsModificar.AutorId;



            _parcialContexto.Entry(postsActual).State = EntityState.Modified;
            _parcialContexto.SaveChanges();

            return Ok(postsModificar);

        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarE(int id)
        {
            posts? posts = (from a in _parcialContexto.posts
                              where a.Id == id
                              select a).FirstOrDefault();

            if (posts == null)
                return NotFound();

            _parcialContexto.posts.Attach(posts);
            _parcialContexto.posts.Remove(posts);
            _parcialContexto.SaveChanges();

            return Ok(posts);
        }

    }
}

