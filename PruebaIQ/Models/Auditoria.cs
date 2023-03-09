using System.ComponentModel.DataAnnotations;

namespace PruebaIQ.Models
{
    public class Auditoria
    {
        [Key]
        public int Id { get; set; }
        public string? NameUser { get; set; }
        public DateTime? DateRegister { get; set; }
    }
}
