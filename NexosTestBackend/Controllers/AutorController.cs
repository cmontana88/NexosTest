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
    public class AutorController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IService<Autor> _serviceAutor;
        private readonly IMapper _mapper;
        public AutorController(IService<Autor> serviceAutor, IMapper mapper)
        {
            _serviceAutor = serviceAutor;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var autores = _serviceAutor.GetAll();
                var autoresDtos = _mapper.Map<IEnumerable<AutorDto>>(autores);

                return Ok(autoresDtos);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error al obtener el listado de autores.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetId(int id)
        {
            try
            {
                var autor = _serviceAutor.Get(id);
                var autorDto = _mapper.Map<AutorDto>(autor);

                return Ok(autorDto);
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error al obtener el autor con id = {id}.");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] AutorDto autorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ModelState.Values.SelectMany(v => v.Errors));
                }

                var autor = _mapper.Map<Autor>(autorDto);
                _serviceAutor.Insert(autor);

                return StatusCode((int)HttpStatusCode.Created, "Autor creado con exito.");
            }
            catch(BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error al crear el autor.");
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] AutorDto autorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ModelState.Values.SelectMany(v => v.Errors));
                }

                var autor = _mapper.Map<Autor>(autorDto);
                _serviceAutor.Update(autor);

                return StatusCode((int)HttpStatusCode.Created, "Autor actualizado con exito.");
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error al actualizar el autor.");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _serviceAutor.Delete(id);
                
                return StatusCode((int)HttpStatusCode.NoContent, "Autor eliminado con exito.");
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + ex.InnerException.Message ?? string.Empty);
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error al eliminar el autor con id = {id}.");
            }
        }
    }
}
