using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace test2.Services
{
    public class NaverEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string? id = "dkaskgkdua@naver.com";
            string? password = "1234";


            SmtpClient client = new SmtpClient
            {
                Host = "smtp.naver.com",// client.Host처럼 개별 속성이름 대신에 중괄호안에서 처리
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(id, password),
                UseDefaultCredentials = false,
                EnableSsl = true
            };

            MailMessage mail = new MailMessage(id, email)
            {
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            return client.SendMailAsync(mail);

        }
    }
}
