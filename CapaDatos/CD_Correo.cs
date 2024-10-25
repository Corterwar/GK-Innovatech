using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Correo : CD_RecuperarPass
    {
        public CD_Correo() {
            //Cambiar estos datos al correo nuevo que hay que hacer de la empresa
            remitenteCorreo = "@gmail.com";
            password = "";
            host = "smtp.gmail.com";
            port = 587;
            ssl = true;
            initializeSmtpClient();
        }
    }
}
