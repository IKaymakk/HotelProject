using System.Net.Mail;
using System.Net;
using HotelProject.Models;
namespace BusinessLayer.Services
{
    public class EmailService
    {
        public async Task<bool> SendMailAsync(MailModel model)
        {
            try
            {
                string smtpSunucu = "smtp.gmail.com";
                int smtpPort = 587;
                string senderMail = "erayyciftci@gmail.com";
                string senderPassword = "okcx wktc avlx gydp";

                using (SmtpClient client = new SmtpClient(smtpSunucu, smtpPort))
                {
                    client.Credentials = new NetworkCredential(senderMail, senderPassword);
                    client.EnableSsl = true;

                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(senderMail);
                        mail.To.Add(model.Email);
                        mail.Subject = model.Subject;
                        mail.IsBodyHtml = true;

                        mail.Body = $@"
                  
                        <div class='container'>
                            <div class='header'>Yeni Mesaj Bildirimi</div>
                            <div class='content'>
                                <p><strong>Ad Soyad:</strong> {model.Name} {model.Lastname}</p>
                                <p><strong>E-Posta:</strong> {model.Email}</p>
                                <p><strong>Telefon:</strong> {model.Phone}</p>
                                <p><strong>Mesaj:</strong> {model.Body}</p>
                            </div>
                        </div>
                   ";

                        await client.SendMailAsync(mail);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
