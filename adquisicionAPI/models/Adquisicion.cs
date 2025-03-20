using System.ComponentModel.DataAnnotations; // Para el atributo [Key]
using System.ComponentModel.DataAnnotations.Schema; // Para el atributo [DatabaseGenerated]

namespace adquisicionAPI.Models
{
        public class Adquisicion
    {
        [Key] // Indica que esta propiedad es la clave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Indica que es autonumérico
        public int Id { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El presupuesto debe ser un valor positivo.")]
        public decimal Presupuesto { get; set; }

        [Required(ErrorMessage = "La unidad es obligatoria.")]
        [StringLength(100, ErrorMessage = "La unidad no puede exceder los 100 caracteres.")]
        public string Unidad { get; set; }

        [Required(ErrorMessage = "El tipo de bien es obligatorio.")]
        [StringLength(100, ErrorMessage = "El tipo de bien no puede exceder los 100 caracteres.")]
        public string TipoDeBien { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El valor unitario debe ser un valor positivo.")]
        public decimal ValorUnitario { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El valor total debe ser un valor positivo.")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "La fecha de adquisición es obligatoria.")]
        public DateTime FechaAdquisicion { get; set; }

        [Required(ErrorMessage = "El proveedor es obligatorio.")]
        [StringLength(200, ErrorMessage = "El nombre del proveedor no puede exceder los 200 caracteres.")]
        public string Proveedor { get; set; }

        [StringLength(500, ErrorMessage = "La documentación no puede exceder los 500 caracteres.")]
        public string Documentacion { get; set; }

        public bool Activo { get; set; }
    }
}