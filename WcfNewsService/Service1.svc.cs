using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace WcfNewsService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Service1 : IServiceNews
    {
        NewsRecipient NewsRecipient;

        public List<News> getNews(string url)
        {
            try
            {
                //string  = "https://news.tut.by/rss/index.rss";
                NewsRecipient = new NewsRecipient(url);
                if (NewsRecipient.GetXmlNews())
                {
                    if (NewsRecipient.ReadXml())
                    {
                        return NewsRecipient.articles;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
                //MessageBox.Show(ex.Message);
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
                //MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool ReadXml()
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
                return true;
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
