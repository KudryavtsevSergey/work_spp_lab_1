using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;

namespace NewsService
{
    public class NewsRecipient
    {
        public List<News> articles = new List<News>();
        private XmlNodeList nodeList;
        private static NewsRecipient instance;

        private static object syncRoot = new Object();


        public static NewsRecipient GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new NewsRecipient();
                }
            }
            return instance;
        }

        public List<News> GetNews(string url)
        {
            this.GetXmlNews(url);
            this.ReadXml();
            if (this.articles == null)
            {
                throw new Exception("Ошибка получения новостей.");
            }
            return this.articles;
        }

        private void GetXmlNews(string url)
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
            }
            catch (NotSupportedException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (XmlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ReadXml()
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

        public string GetNewsSerialaized(string url)
        {
            try
            {
                GetNews(url);
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
            catch(InvalidDataContractException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(SerializationException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}