using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaIQ.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Domain.Entities;
using PruebaIQ.Services.Interfaces;
using System.Text;

namespace PruebaIQ.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly string _secretKey;
        private readonly IUsuarioServices _usuarioServices;
        private readonly IAuditoriaServices _auditoriaServices;

        public LoginController(IConfiguration config, 
                               IUsuarioServices usuarioServices,
                               IAuditoriaServices auditoriaServices)
        {
            _secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
            _usuarioServices = usuarioServices;
            _auditoriaServices = auditoriaServices;
        }

        [HttpPost]
        [Route("Validar")]
        public IActionResult Validar([FromBody] Login request)
        {
            Models.Usuario usuario = new Models.Usuario();
            BaseResponse baseResponseToken = new BaseResponse();
            BaseResponse baseResponse = _usuarioServices.GetByName(request.nameuser);
            if (baseResponse.response) 
            {
                usuario = (Models.Usuario)baseResponse.result;
            }
            if (request.nameuser == usuario.NameUser && request.pass == usuario.Password)
            {
                var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.nameuser));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                baseResponseToken = new BaseResponse()
                {
                    response = true,
                    message = "Token generado con exito",
                    token = tokenCreado
                };

                Models.Auditoria auditoria = new Models.Auditoria()
                {
                    NameUser = usuario.NameUser,
                    DateRegister = DateTime.UtcNow
                };

                _auditoriaServices.AddAuditoria(auditoria);

                return StatusCode(StatusCodes.Status200OK, baseResponseToken);
            }
            else
            {
                baseResponseToken = new BaseResponse()
                {
                    response = false,
                    message = "Usuario y/o contraseña incorrecta",
                    token = string.Empty
                };

                return StatusCode(StatusCodes.Status401Unauthorized, baseResponseToken);
            }
        }
    }
}
