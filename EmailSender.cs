using System;
using System.Net.Mail;

namespace YourNameSpace
{
    public class EmailResult {
        public bool Success {get; set;}
        public string Message { get; set; }
    }

    public class EmailSender
    {     
        public bool SendEmail(string From, string To, string Pass, string Subject, string Body)
        {
            SmtpClient smtpServer = new SmtpClient();
            MailMessage mail = new MailMessage();
            EmailResult result =  new EmailResult();

            try
            {
                if (!string.IsNullOrEmpty(To) && !string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(Pass)) && !string.IsNullOrEmpty(Subject)
                {
                    smtpServer.Host = "smtp.gmail.com";               
                    smtpServer.Port = 587;
                    smtpServer.EnableSsl = true;               
                    smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;               
                    smtpServer.UseDefaultCredentials = false;
                    smtpServer.Credentials = new System.Net.NetworkCredential(From, Pass);

                    mail = new MailMessage();
                    mail.From = new MailAddress(this.From);
                    mail.To.Add(To);                   
                    mail.Subject = Subject;
                    mail.Body = Body + "<br /><br /><b>Este mensaje fue generado por un sistema automatizado. Por favor, no respondas a este mensaje.</b>";
                    mail.IsBodyHtml = true;
                
                    smtpServer.Send(mail);
                
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception exc)
            {
                result.Success = false;
                result.Message = exc.Message.ToString();

                return result;
            }
        }
    }
}
