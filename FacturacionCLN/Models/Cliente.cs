using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacturacionCLN.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")] // Define el tipo de columna en la base de datos
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Nombre { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Direccion { get; set; }

        [Required]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El número de celular debe contener exactamente 8 dígitos.")]
        [Column(TypeName = "varchar(8)")]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico es inválido.")]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Required]
        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string CodigoPais { get; set; }
    }
}
