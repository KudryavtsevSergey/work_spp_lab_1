using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Threading;

namespace WcfNewsService
{
    public class Service1 : IServiceNews
    {
        NewsRecipient NewsRecipient;

        public List<News> getNews(string url)
        {
            try
            {
                NewsRecipient = new NewsRecipient(url);
                if (NewsRecipient.GetXmlNews())
                {
                    Thread threadXml = new Thread(NewsRecipient.ReadXml);
                    threadXml.Start();
                    threadXml.Join();
                    return NewsRecipient.articles;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string getNewsSerialaized(string url)
        {
            try
            {
                NewsRecipient = new NewsRecipient(url);
                if (NewsRecipient.GetXmlNews())
                {
                    Thread threadXml = new Thread(NewsRecipient.ReadXml);
                    threadXml.Start();
                    threadXml.Join();
                    return NewsRecipient.SerialaizedNews();
                }
                return null;
            }
            catch (Exception ex)
            {
                var t = ex.Message;
                return null;
            }
        }

        public async void SendEmailAsync(News article, string receiversAddress)
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
            catch (Exception)
            {

            }
        }
    }

    public class NewsRecipient
    {
        public List<News> articles = new List<News>();
        private string url;
        private XmlNodeList nodeList;

        public NewsRecipient()
        {
            this.url = "https://news.tut.by/rss/index.rss";
        }

        public NewsRecipient(string url)
        {
            this.url = url;
        }

        public bool GetXmlNews()
        {
            try
            {
                WebRequest wr = WebRequest.Create(url);
                wr.Proxy.Credentials = CredentialCache.DefaultCredentials;
                XmlTextReader xtr = new XmlTextReader(wr.GetResponse().GetResponseStream());
                XmlDocument doc = new XmlDocument();
                doc.Load(xtr);
                XmlNode root = doc.DocumentElement;
                nodeList = root.ChildNodes;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ReadXml()
        {
            try
            {
                foreach (XmlNode chanel in nodeList)
                {
                    foreach (XmlNode chanel_item in chanel)
                    {
                        if (chanel_item.Name == "item")
                        {
                            XmlNodeList itemsList = chanel_item.ChildNodes;
                            News article = new News();

                            foreach (XmlNode item in itemsList)
                            {
 
                                if (item.Name == "title")
                                {
                                    article.TitleValue = item.InnerText;
                                }
                                else if (item.Name == "link")
                                {
                                    article.LinkValue = item.InnerText;
                                }
                                else if (item.Name == "description")
                                {
                                    article.DescriptionValue = item.InnerText;
                                }
                                else if (item.Name == "pubDate")
                                {
                                    article.DateValue = item.InnerText;
                                }
                            }
                            articles.Add(article);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public string SerialaizedNews()
        {
            string resultStr = null;
            using (var output = new StringWriter())
            {
                using (var writer = new XmlTextWriter(output) { Formatting = Formatting.Indented })
                {
                    var dataContractSerializer = new DataContractSerializer(typeof(List<News>));
                    dataContractSerializer.WriteObject(writer, articles);
                    resultStr = output.GetStringBuilder().ToString();
                    return resultStr;
                }
            }
        }
    }
}
