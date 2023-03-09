using Domain;
using Domain.Entities;
using PruebaIQ.Services.Interfaces;

namespace PruebaIQ.Services.Clases
{
    public class AuditoriaServices : IAuditoriaServices
    {
        private readonly PruebaIQDataContext _DataContext;
        public AuditoriaServices(PruebaIQDataContext dataContext)
        {
            _DataContext = dataContext;
        }

        public IEnumerable<Models.Auditoria> GetList()
        {
            var auditorias = _DataContext.auditorias.ToList();

            var list = auditorias.Select(adi => new Models.Auditoria { 
                Id = adi.Id,
                NameUser = adi.NameUser,
                DateRegister = adi.DateRegister
            }).ToList();            

            return list;
            //throw new NotImplementedException();
        }

        public Auditoria AddAuditoria(Models.Auditoria auditoria)
        {
            Auditoria newAuditoria = new Auditoria()
            {
                Id = auditoria.Id,
                NameUser = auditoria.NameUser,
                DateRegister = DateTime.Now.ToLocalTime()
            };

            _DataContext.Add(newAuditoria);
            _DataContext.SaveChanges();

            return newAuditoria;
            //throw new NotImplementedException();
        }
    }
}
