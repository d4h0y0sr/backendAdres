using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace adquisicionAPI.Models
{
    public class Historial
    {
        [Key]
        public int IdHistorial { get; set; }

        [ForeignKey("Adquisicion")]
        public int IdAdquisicion { get; set; }

        [Required]
        [MaxLength(10)]
        public required string Operacion { get; set; }  // 'INSERT' o 'UPDATE'

        [Required]
        public required DateTime FechaCambio { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(100)]
        public string? Usuario { get; set; }

        [Required]
        [JsonIgnore]
        public required string? Cambios { get; set; } = "{}"; // JSON con los cambios

        [NotMapped]
        [JsonProperty("cambios")] // Renombrar CambiosDeserializados para que lo devuelva con el mismo nombre
                public Dictionary<string, CambioDetalle> CambiosDeserializados
        {
            get => string.IsNullOrEmpty(Cambios)
                        ? []
                        : JsonConvert.DeserializeObject<Dictionary<string, CambioDetalle>>(Cambios) ?? [];
            set => Cambios = JsonConvert.SerializeObject(value ?? []);
        }
    }

    // 📌 Clase auxiliar para manejar el JSON de cambios
    public class CambioDetalle
    {
        public required string Antiguo { get; set; }
        public required string Nuevo { get; set; }
    }

}