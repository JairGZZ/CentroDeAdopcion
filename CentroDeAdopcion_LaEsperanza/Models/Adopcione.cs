using System.ComponentModel.DataAnnotations;

namespace CentroDeAdopcion_LaEsperanza.Models;

public partial class Adopcione
{
    public int IdAdopcion { get; set; }

    [Required(ErrorMessage = "Debe especificar la mascota a adoptar.")]
    public int? IdMascota { get; set; }

    [Required(ErrorMessage = "Debe especificar el adoptante.")]
    public int? IdAdoptante { get; set; }

    [Required(ErrorMessage = "La fecha de solicitud es obligatoria.")]
    public DateTime? FechaSolicitud { get; set; }

    public virtual Usuario? IdAdoptanteNavigation { get; set; }

    public virtual Mascota? IdMascotaNavigation { get; set; }
}

