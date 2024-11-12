using ApiServicioCupones.Interfaces;
using System.Net;
using System.Net.Mail;

namespace ApiServicioCupones.Service
{
    public class SendEmailService : ISendEmailService
    {
        public async Task EnviarEmailCliente(string emailCliente, string nroCupon)
        {
            string emailEmisor = "api.cuponeraprogiv.unlz@gmail.com"; // crear un mail, como en PHP (Apicuponera)
            string emailCodigo = "fjyt fwiu jwwy sehs"; // generar clave de aplicacion, como en PHP
            string servicioGoogle = "smtp.gmail.com";

            try
            {
                SmtpClient smtpClient = new SmtpClient(servicioGoogle);
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential (emailEmisor, emailCodigo);
                smtpClient.EnableSsl = true;

                MailMessage message = new MailMessage();

                message.From = new MailAddress(emailEmisor, "Servicios atencion al cliente");
                message.To.Add(emailCliente);
                message.Subject = "Numero de cupon asignado"; // el asunto del mail.
                message.Body = $"El numero de cupon asignado es: {nroCupon}"; // cuerpo del Email

                await smtpClient.SendMailAsync(message);

            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
