using Domain.Entities;

namespace PruebaIQ.Services.Interfaces
{
    public interface IAuditoriaServices
    {
        public IEnumerable<Models.Auditoria> GetList();

        Auditoria AddAuditoria(Models.Auditoria auditoria);
    }
}
