using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Auditoria : Entidad<int>
    {
        [StringLength(100)]
        public string? NameUser { get; set; }
        [StringLength(100)]
        public DateTime? DateRegister { get; set; }
    }
}
