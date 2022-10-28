using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Application.Sender
{
    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("mohammad.h.spour@gmail.com", "بکتاکالا");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);
           
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("mohammad.h.spour@gmail.com", "zfxpwxypbnyzhcae");
            SmtpServer.EnableSsl = true;
           
            SmtpServer.Send(mail);

        }
    }
}