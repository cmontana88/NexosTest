using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexosTestBackend.Core.DTOs;
using NexosTestBackend.Core.Exceptions;
using NexosTestBackend.Core.Interfaces;
using NexosTestBackend.Data.Entidades;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NexosTestBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IService<Libro> _serviceLibro;
        private readonly IMapper _mapper;
        public LibroController(IService<Libro> serviceLibro, IMapper mapper)
        {
            _serviceLibro = serviceLibro;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var libros = _serviceLibro.GetAll();
                var librosDtos = _mapper.Map<IEnumerable<LibroDto>>(libros);

                return Ok(librosDtos);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error al obtener el listado de libros.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetId(int id)
        {
            try
            {
                var libro = _serviceLibro.Get(id);
                var libroDto = _mapper.Map<LibroDto>(libro);

                return Ok(libroDto);
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error al obtener el libro con id = {id}.");
            }
        }

        [HttpGet("{page}/{itemxpage}/{fieldFilter}/{criterioFilter}")]
        public ActionResult GetFilter(int page, int itemxpage, string fieldFilter = "", string criterioFilter = "")
        {
            try
            {
                var productos = ((ILibroService)_serviceLibro).ObtenerTodosLosLibrosFiltrados(fieldFilter, criterioFilter.Equals("ninguno") ? string.Empty : criterioFilter, page, itemxpage);
                return Ok(productos);
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error al obtener los libros filtrados.");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] LibroDto libroDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ModelState.Values.SelectMany(v => v.Errors));
                }

                var libro = _mapper.Map<Libro>(libroDto);
                _serviceLibro.Insert(libro);

                return StatusCode((int)HttpStatusCode.Created, "Libro creado con exito.");
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error al crear el libro.");
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] LibroDto libroDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ModelState.Values.SelectMany(v => v.Errors));
                }

                var libro = _mapper.Map<Libro>(libroDto);
                _serviceLibro.Update(libro);

                return StatusCode((int)HttpStatusCode.Created, "Libro actualizado con exito.");
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error al actualizar el libro.");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _serviceLibro.Delete(id);

                return StatusCode((int)HttpStatusCode.NoContent, "Libro eliminado con exito.");
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error al eliminar el libro con id = {id}.");
            }
        }
    }
}
