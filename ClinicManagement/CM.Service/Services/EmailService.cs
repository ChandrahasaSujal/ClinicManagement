using CM.Data.ViewModels.Appointment;
using CM.Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CM.Service.Services
{
    public class EmailService : IEmailService
    {
        public bool SendAppointmentEMail(AppointmentViewModel appointee)
        {
            try
            {
                if (appointee!=null)
                {
                    var email = System.Configuration.ConfigurationManager.AppSettings["email"].ToString();
                    var password = System.Configuration.ConfigurationManager.AppSettings["password"].ToString();
                    var smtpServer = System.Configuration.ConfigurationManager.AppSettings["smtpServer"].ToString();
                    int.TryParse(System.Configuration.ConfigurationManager.AppSettings["smtpPort"], out int smtpPort);

                    SmtpClient client = new SmtpClient(smtpServer, smtpPort);
                    client.EnableSsl = true;
                    client.Timeout = 100000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(email, password);

                    var mail = PrepareMessage(email,appointee);
                    if (mail!=null)
                    {
                        client.Send(mail);
                        return true;
                    }
                }
                return false;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private MailMessage PrepareMessage(string fromToEmail, AppointmentViewModel appointment)
        {
            try
            {
                MailMessage mail = new MailMessage(fromToEmail, fromToEmail);
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;

                mail.Subject = "Appointment at" + DateTime.Now.ToString("ddMMyy hh:mm::ss");
                var emailBody = File.ReadAllText(@"~/CM.Service/EmailTemplates/AppointmentTemplate.html");

                // Replace the actual data with {data} from html file.

                emailBody.Replace("{name}", appointment.Name);
                emailBody.Replace("{gender}", appointment.Gender.ToString());
                emailBody.Replace("{DOA}", appointment.DOA);
                emailBody.Replace("{PhoneNumber}", appointment.Phone);
                emailBody.Replace("{email}", appointment.MailId??string.Empty);

                mail.Body = emailBody;
                return mail;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
