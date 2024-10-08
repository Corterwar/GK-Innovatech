using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Cupon
    {

        public List<Cupon> Listar()
        {
            List<Cupon> lista = new List<Cupon>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.IdCupon, c.Codigo, c.Descuento, c.Estado, c.FechaRegistro from Cupon c");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);

                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cupon()
                            {
                                IdCupon =  Convert.ToInt32(dr["IdCupon"]),
                                Codigo = dr["Codigo"].ToString(),
                                Descuento = Convert.ToInt32(dr["Descuento"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Cupon>();
                }
            }
            return lista;
        }

        public int Registrar(Cupon obj, out string Mensaje)
        {
            int IdCupon = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCUPON", oconexion);


                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Descuento", obj.Descuento);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    IdCupon = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                IdCupon = 0;
                Mensaje = ex.Message;
            }

            return IdCupon;

        }

        public bool Editar(Cupon obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {


                    SqlCommand cmd = new SqlCommand("SP_EDITARCUPON", oconexion);
                    cmd.Parameters.AddWithValue("IdCupon", obj.IdCupon);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Descuento", obj.Descuento);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;

        }

        public bool Eliminar(Cupon obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {


                    SqlCommand cmd = new SqlCommand("SP_ELIMINARCUPON", oconexion);
                    cmd.Parameters.AddWithValue("IdCupon", obj.IdCupon);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;

        }
    }
}
