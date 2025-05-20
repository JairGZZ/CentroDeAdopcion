using System.ComponentModel.DataAnnotations;

namespace CentroDeAdopcion_LaEsperanza.ViewModels;

public class UsuarioViewModel
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string Apellido { get; set; }

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "Debe ser un email válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "Debe ser un número de teléfono válido.")]
    public string Telefono { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    public string Direccion { get; set; }

    [Required(ErrorMessage = "El rol es obligatorio.")]
    public string Rol { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6,
        ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string Contrasena { get; set; } = null!;

    [Required(ErrorMessage = "Debe confirmar la contraseña.")]
    [DataType(DataType.Password)]
    [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmarContrasena { get; set; } = null!;
}
