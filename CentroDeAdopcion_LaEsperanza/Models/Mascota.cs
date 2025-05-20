using System.ComponentModel.DataAnnotations;

namespace CentroDeAdopcion_LaEsperanza.Models;
public partial class Mascota
{
    public int IdMascota { get; set; }

    [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El tipo de mascota es obligatorio.")]
    public string Tipo { get; set; } = null!;

    public string? Raza { get; set; }

    [Required(ErrorMessage = "La edad es obligatoria.")]
    [Range(1, 19, ErrorMessage = "La edad debe ser entre 1 y 19 años.")]
    public int? Edad { get; set; }

    public string? Tamaño { get; set; }

    public string? Sexo { get; set; }

    [Required(ErrorMessage = "El estado de salud es obligatorio.")]
    public string? EstadoSalud { get; set; }

    public string? Estado { get; set; }

    public string? Foto { get; set; }

    public int? IdPropietario { get; set; }

    public virtual ICollection<Adopcione> Adopciones { get; set; } = new List<Adopcione>();

    public virtual Usuario? IdPropietarioNavigation { get; set; }
}
