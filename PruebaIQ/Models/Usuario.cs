using System.ComponentModel.DataAnnotations;

namespace PruebaIQ.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string NameUser { get; set; }
        public string Password { get; set; }
    }
}
