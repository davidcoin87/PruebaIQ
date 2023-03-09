using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaIQ.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaIQ.Controllers
{
    [Route("api/Auditoria")]
    [Authorize]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAuditoriaServices _auditoriaServices;

        public AuditoriaController(IAuditoriaServices auditoriaServices)
        {
            _auditoriaServices = auditoriaServices;
        }

        // GET: api/<ValuesController>
        [HttpGet(Name = "GetAuditoriaList")]
        //public IActionResult GetList() => Ok(_auditoriaServices.GetList());

        public IActionResult GetList()
        {
            var list = _auditoriaServices.GetList();
            return Ok(list);
        }

        // GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Models.Auditoria auditoria)
        {   
            _auditoriaServices.AddAuditoria(auditoria);
        }

        // PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
