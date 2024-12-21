﻿namespace CentroDeAdopcion_LaEsperanza.ViewModels
{
    public class UsuarioViewModel
    {
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }
        public string Rol { get; set; } = null!;
        public string Contrasena { get; set; } = null!;

        public string ConfirmarContrasena { get; set; } = null!;


    }
}
