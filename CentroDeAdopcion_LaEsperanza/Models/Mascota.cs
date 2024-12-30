using System.ComponentModel.DataAnnotations;

namespace CentroDeAdopcion_LaEsperanza.Models;

public partial class Mascota
{
    public int IdMascota { get; set; }

    public string? Nombre { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Raza { get; set; }

   [Required(ErrorMessage = "La edad es obligatoria.")]
    [Range(1, 19, ErrorMessage = "La edad debe ser mayor a 0 y menor a 20.")]
    public int? Edad { get; set; }
    public string? Tamaño { get; set; }

    public string? Sexo { get; set; }

    public string? EstadoSalud { get; set; }

    public string? Foto { get; set; }

    public int? IdPropietario { get; set; }

    public virtual ICollection<Adopcione> Adopciones { get; set; } = new List<Adopcione>();

    public virtual Usuario? IdPropietarioNavigation { get; set; }
}
