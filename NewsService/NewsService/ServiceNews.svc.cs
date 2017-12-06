using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace NewsService
{
    public class ServiceNews : IServiceNews
    {
        NewsRecipient NewsRecipient = NewsRecipient.GetInstance();

        public List<News> GetNews(Uri uri)
        {
            try
            {
                return NewsRecipient.GetNews(uri);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetNewsSerialaized(Uri uri)
        {
            try
            {
                return NewsRecipient.GetNewsSerialaized(uri);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async void SendEmail(News article, string receiversAddress)
        {
            try
            {
                string sendersAddress = "invaderman@yandex.ru";
                const string sendersPassword = "qazwsx147258369";
                const string subject = "New";
                string body = "Здравствуйте!" + Environment.NewLine;
                body += article.TitleValue + Environment.NewLine + article.DateValue + Environment.NewLine + article.LinkValue + Environment.NewLine;
                SmtpClient smtp = new SmtpClient { Host = "smtp.yandex.ru", Port = 25, EnableSsl = true, DeliveryMethod = SmtpDeliveryMethod.Network, Credentials = new NetworkCredential(sendersAddress, sendersPassword), Timeout = 3000 };
                MailMessage message = new MailMessage(sendersAddress, receiversAddress, subject, body);
                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
