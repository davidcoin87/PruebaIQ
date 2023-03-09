using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PruebaIQ.Models;
using PruebaIQ.Services.Clases;
using PruebaIQ.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaIQ.Controllers
{
    [Route("api/Usuario")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioServices;

        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }


        //// GET: api/<UsuarioController>
        [HttpGet]
        public IActionResult Get()
        {
            var list = _usuarioServices.GetList();
            return Ok(list);
        }

        //// GET api/<UsuarioController>/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var user = _usuarioServices.GetById(id);
            return Ok(user);
        }

        //// GET api/<UsuarioController>/Nombre
        //[Route("nombre")]
        //[HttpGet("{usuario}")]
        //public IActionResult Get(string usuario)
        //{
        //    var user = _usuarioServices.GetByName(usuario);
        //    return Ok(user);
        //}

        //// POST api/<UsuarioController>
        [HttpPost]
        public IActionResult Post([FromBody] Models.Usuario usuario)
        {
            BaseResponse baseResponse = _usuarioServices.AddUsuario(usuario);

            if (!baseResponse.response)
            {
                return BadRequest(baseResponse);
            }
            
            return Ok(baseResponse);
        }

        //// PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Models.Usuario usuario)
        {
            BaseResponse baseResponse = _usuarioServices.EditUsuario(usuario, id);

            if (!baseResponse.response)
            {
                return BadRequest(baseResponse);
            }

            return Ok(baseResponse);
        }

        //// DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            BaseResponse baseResponse = _usuarioServices.DeleteUsuario(id);

            if (!baseResponse.response)
            {
                return BadRequest(baseResponse);
            }

            return Ok(baseResponse);
        }
    }
}
