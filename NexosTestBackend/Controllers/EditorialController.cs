using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexosTestBackend.Core.DTOs;
using NexosTestBackend.Core.Exceptions;
using NexosTestBackend.Core.Interfaces;
using NexosTestBackend.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NexosTestBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialController : ControllerBase
    {
        private readonly IService<Editorial> _serviceEditorial;
        private readonly IMapper _mapper;
        public EditorialController(IService<Editorial> serviceEditorial, IMapper mapper)
        {
            _serviceEditorial = serviceEditorial;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var editoriales = _serviceEditorial.GetAll();
                var editorialesDtos = _mapper.Map<IEnumerable<EditorialDto>>(editoriales);

                return Ok(editorialesDtos);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error al obtener el listado de editoriales.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetId(int id)
        {
            try
            {
                var editorial = _serviceEditorial.Get(id);
                var editorialDto = _mapper.Map<EditorialDto>(editorial);

                return Ok(editorialDto);
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error al obtener la editorial con id = {id}.");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] EditorialDto editorialDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ModelState.Values.SelectMany(v => v.Errors));
                }

                var autor = _mapper.Map<Editorial>(editorialDto);
                _serviceEditorial.Insert(autor);

                return StatusCode((int)HttpStatusCode.Created, "Editorial creada con exito.");
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error al crear la editorial.");
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] EditorialDto editorialDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, ModelState.Values.SelectMany(v => v.Errors));
                }

                var editorial = _mapper.Map<Editorial>(editorialDto);
                _serviceEditorial.Update(editorial);

                return StatusCode((int)HttpStatusCode.Created, "Editorial actualizada con exito.");
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error al actualizar la editorial.");
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                _serviceEditorial.Delete(id);

                return StatusCode((int)HttpStatusCode.NoContent, "Editorial eliminada con exito.");
            }
            catch (BusinessException ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Error al eliminar la editorial con id = {id}.");
            }
        }
    }
}
