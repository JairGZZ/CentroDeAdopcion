namespace CentroDeAdopcion_LaEsperanza.Models;

public partial class Adopcione
{
    public int IdAdopcion { get; set; }

    public int? IdMascota { get; set; }

    public int? IdAdoptante { get; set; }

    public DateTime? FechaSolicitud { get; set; }

    public virtual Usuario? IdAdoptanteNavigation { get; set; }

    public virtual Mascota? IdMascotaNavigation { get; set; }
}
