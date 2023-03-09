using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using PruebaIQ.Models;

namespace PruebaIQ.Services.Interfaces
{
    public interface IUsuarioServices
    {
        public IEnumerable<Models.Usuario> GetList();
        BaseResponse GetById(int id);
        BaseResponse GetByName(string user);
        BaseResponse AddUsuario(Models.Usuario usuario);
        BaseResponse EditUsuario(Models.Usuario usuario, int id);
        BaseResponse DeleteUsuario(int id);
    }
}
