using System.ComponentModel.DataAnnotations;

namespace TestDanaide.Models
{
    public class CreateCartRequest
    {

        [Required]
        public string DNI { get; set; }

    }
}
