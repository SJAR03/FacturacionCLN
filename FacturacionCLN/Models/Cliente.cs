using System.ComponentModel.DataAnnotations;

namespace FacturacionCLN.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Direccion { get; set; }

        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El número de celular debe contener exactamente 8 dígitos.")]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico es inválido.")]
        public string Email { get; set; }

        [Required]
        [StringLength(3)]
        public string CodigoPais { get; set; }
    }
}
