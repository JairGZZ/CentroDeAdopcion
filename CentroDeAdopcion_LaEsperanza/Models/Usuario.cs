namespace CentroDeAdopcion_LaEsperanza.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Email { get; set; }

    public string Telefono { get; set; }

    public string Direccion { get; set; }

    public string Rol { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? FotoUrl { get; set; }

    public virtual ICollection<Adopcione> Adopciones { get; set; } = new List<Adopcione>();

    public virtual ICollection<Mascota> Mascota { get; set; } = new List<Mascota>();
}

  
