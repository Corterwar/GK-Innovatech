using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Recuperar
    {

        public string recoverPassword(string userRequesting)
        {
            return new CD_RecuperarPass().recoverPassword(userRequesting);
        }

        public string recoverPassword2(string userRequesting, string token)
        {
            return new CD_RecuperarPass().recoverPassword2(userRequesting, token.ToString());
        }

    }
}
