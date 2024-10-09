using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cupon
    {
        private CD_Cupon objcd_cupon = new CD_Cupon();

        public List<Cupon> obtenerCupones()
        {
            return objcd_cupon.Listar();
        }

        public int Registrar(Cupon obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Codigo == "")
            {
                Mensaje += "Es Necesario colocar un codigo valido para el cupon\n";
            }

            if (!(obj.Descuento >= 0 && obj.Descuento <= 100))
            {
                Mensaje += "Es Necesario colocar un valor valido para el descuento\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_cupon.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Cupon obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Codigo == "")
            {
                Mensaje += "Es Necesario colocar un codigo valido para el cupon\n";
            }


            if (!(obj.Descuento >= 0 && obj.Descuento <= 100))
            {
                Mensaje += "Es Necesario colocar un valor valido para el descuento\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_cupon.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Cupon obj, out string Mensaje)
        {
            return objcd_cupon.Eliminar(obj, out Mensaje);
        }

    }
}
