using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailReminder
{
    class Program
    {
        static void Main(string[] args)
        {
            EmailFormModel model=new EmailFormModel();
            model.FromName = "Server";
            model.FromEmail = "usermail@gmail.com";
            model.Message = "Reminding operation.this is test message." + DateTime.Now.ToShortTimeString();
            Task t = SendMailReminder(model);
            t.ContinueWith((str) =>
            {
                Console.WriteLine(str.Status.ToString());
            });
            
            t.Wait();
        }
        public async static Task<string> SendMailReminder(EmailFormModel model)
        {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("ash5rifat@gmail.com"));  // replace with valid value 
                message.From = new MailAddress("usermail@gmail.com");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "usermail@gmail.com",  // replace with valid value
                        Password = "password"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return "conplete";
                }
        }
        public class EmailFormModel
        {
            public string FromName { get; set; }
            public string FromEmail { get; set; }
            public string Message { get; set; }
        }
    }
}
