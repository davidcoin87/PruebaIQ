using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using PruebaIQ.Helpers;
using PruebaIQ.Models;
using PruebaIQ.Services.Interfaces;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PruebaIQ.Services.Clases
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly PruebaIQDataContext _DataContext;

        public UsuarioServices(PruebaIQDataContext dataContext)
        {
            _DataContext = dataContext;
        }

        public IEnumerable<Models.Usuario> GetList()
        {
            var usuarios = _DataContext.usuarios.ToList();

            var list = usuarios.Select(user => new Models.Usuario
            {
                Id = user.Id,
                NameUser = user.NameUser,
                Password = user.Password
            }).ToList();

            return list;
            //throw new NotImplementedException();
        }        

        public BaseResponse GetById(int id)
        {
            Models.Usuario newUser = new Models.Usuario();
            BaseResponse baseResponse = new BaseResponse();
            var user = _DataContext.usuarios.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                newUser = new Models.Usuario()
                {
                    Id = user.Id,
                    NameUser = user.NameUser,
                    Password = user.Password
                };

                baseResponse = new BaseResponse()
                {
                    response = true,
                    message = "Dato conseguido con exito",
                    result = newUser
                };
            }
            else
            {
                baseResponse = new BaseResponse()
                {
                    response = false,
                    message = "Usuario no existe",
                    result = null
                };
            }
            
            return baseResponse;
            //throw new NotImplementedException();
        }

        public BaseResponse GetByName(string user)
        {
            Models.Usuario newUser = new Models.Usuario();
            BaseResponse baseResponse = new BaseResponse();
            Domain.Entities.Usuario usuario = _DataContext.usuarios.FirstOrDefault(x => x.NameUser == user);
            if(usuario != null)
            {
                var pass = Encryption.Decrypt(usuario.Password);

                newUser = new Models.Usuario()
                {
                    Id = usuario.Id,
                    NameUser = usuario.NameUser,
                    Password = pass
                };

                baseResponse = new BaseResponse()
                {
                    response = true,
                    message = "Dato conseguido con exito",
                    result = newUser
                };
            }
            else
            {
                baseResponse = new BaseResponse()
                {
                    response = false,
                    message = "Usuario no existe",
                    result = null
                };
            }

            return baseResponse;
            //throw new NotImplementedException();
        }

        public BaseResponse AddUsuario(Models.Usuario usuario)
        {
            BaseResponse baseResponse = new BaseResponse();
            var pass = Encryption.Encrypt(usuario.Password);
            Domain.Entities.Usuario newUsuario = new Domain.Entities.Usuario()
            {
                Id = usuario.Id,
                NameUser = usuario.NameUser,
                Password = pass
            };

            _DataContext.Add(newUsuario);
            _DataContext.SaveChanges();

            baseResponse = new BaseResponse()
            {
                response = true,
                message = "Usuario agregado con éxito",
                result = newUsuario
            };

            return baseResponse;
            //throw new NotImplementedException();
        }

        public BaseResponse EditUsuario(Models.Usuario usuario, int id)
        {
            BaseResponse baseResponse = new BaseResponse();
            var pass = Encryption.Encrypt(usuario.Password);
            var existe = _DataContext.usuarios.Any(x => x.Id == id);

            if (!existe)
            {
                baseResponse = new BaseResponse()
                {
                    response = false,
                    message = "Usuario no existe",
                    result = null
                };
                return baseResponse;
            }

            if (usuario.Id != id)
            {
                baseResponse = new BaseResponse()
                {
                    response = false,
                    message = "Los identificadores no coinciden",
                    result = null
                };
                return baseResponse;
            }

            Domain.Entities.Usuario newUsuario = new Domain.Entities.Usuario()
            {
                Id = usuario.Id,
                NameUser = usuario.NameUser,
                Password = pass
            };

            //_DataContext.Entry(newUsuario).State = EntityState.Modified;

            _DataContext.Update(newUsuario);
            _DataContext.SaveChanges();

            baseResponse = new BaseResponse()
            {
                response = true,
                message = "Usuario actualizado con éxito",
                result = usuario
            };

            return baseResponse;
            //throw new NotImplementedException();
        }

        public BaseResponse DeleteUsuario(int id)
        {
            BaseResponse baseResponse = new BaseResponse();
            var existe = _DataContext.usuarios.Any(x => x.Id == id);

            if (!existe)
            {
                baseResponse = new BaseResponse()
                {
                    response = false,
                    message = "Usuario no existe",
                    result = null
                };
            }
            else
            {
                _DataContext.Remove(new Domain.Entities.Usuario() { Id = id });
                _DataContext.SaveChanges();

                baseResponse = new BaseResponse()
                {
                    response = true,
                    message = "Usuario eliminado con exito",
                    result = null
                };
            }

            return baseResponse;
        }

        
    }
}
