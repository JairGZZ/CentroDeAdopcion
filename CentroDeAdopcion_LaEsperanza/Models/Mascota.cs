namespace CentroDeAdopcion_LaEsperanza.Models;

public partial class Mascota
{
    public int IdMascota { get; set; }

    public string? Nombre { get; set; }

    public string Tipo { get; set; } = null!;

    public string? Raza { get; set; }

    public int? Edad { get; set; }

    public string? Tamaño { get; set; }

    public string? Sexo { get; set; }

    public string? EstadoSalud { get; set; }

    public string? Foto { get; set; }

    public int? IdPropietario { get; set; }

    public virtual ICollection<Adopcione> Adopciones { get; set; } = new List<Adopcione>();

    public virtual Usuario? IdPropietarioNavigation { get; set; }
}
