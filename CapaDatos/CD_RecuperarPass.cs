using CapaEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_RecuperarPass
    {

        private SmtpClient smtpClient;
        protected String remitenteCorreo { get; set; }
        protected String password { get; set; }
        protected String host { get; set; }
        protected int port { get; set; }
        protected bool ssl { get; set; }



        protected void initializeSmtpClient()
        {
            smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential(remitenteCorreo,password);
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
        }

        public void sendMail(string subject, string body, List<String> destinatarioCorreo)
        {
            var mailMessage = new MailMessage();
            try
            {
                mailMessage.From = new MailAddress(remitenteCorreo);

                foreach (string mail in destinatarioCorreo)
                {
                    mailMessage.To.Add(mail);
                }

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.Priority = MailPriority.Normal;
                smtpClient.Send(mailMessage);

            }catch (Exception ex) {}
            finally
            { 
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }


        public string recoverPassword(string usuarioSolicitando)
        {
            using (SqlConnection oconexion = new SqlConnection())
            {
                oconexion.ConnectionString = Conexion.cadena;
                oconexion.Open();

                    using (var command = new SqlCommand())
                    {

                        command.Connection = oconexion;
                        command.CommandText = "Select * from Usuario where Documento = @Dni";
                        command.Parameters.AddWithValue("@Dni", usuarioSolicitando);
                        command.CommandType = CommandType.Text;

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read() == true)
                        {
                            string nombreUsuario = reader.GetString(2);
                            string correoUsuario = reader.GetString(3);
                            string password = reader.GetString(4);

                            var mailService = new CD_Correo();
                            mailService.sendMail(subject:"Sistema de ventas GK: Solicitud de recuperacion de contraseña",
                                body: "Hola, " + nombreUsuario + "\nUsted solicito recuperar su contraseña. \n", destinatarioCorreo: new List<string> { correoUsuario}
                            );

                            return "Revise su correo";

                        }
                        else
                        {
                            return "No hay ningun usuario con esas credenciales";
                        }
                    }
                
            }
        }

    }
}
