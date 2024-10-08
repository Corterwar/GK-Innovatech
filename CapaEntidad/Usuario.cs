﻿using System;

namespace CapaEntidad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public String Documento { get; set; }
        public String NombreCompleto { get; set; }
        public String Correo { get; set; }
        public String Clave { get; set; }
        public Rol oRol { get; set; }
        public bool Estado { get; set; }
        public String FechaRegistro { get; set; }
    }
}
