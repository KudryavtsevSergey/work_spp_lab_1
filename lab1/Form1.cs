using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listNews.FullRowSelect = true;
            Type type = listNews.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(listNews, true, null);

            getNews();
        }
        NewsRecipient NewsRecipient;

        private void timer_Tick(object sender, EventArgs e)
        {
            getNews();
        }

        private void listNews_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(NewsRecipient.articles[Int32.Parse(listNews.SelectedItems[0].Text)].link);
        }

        private async void getNews()
        {
            try
            {
                string url = "https://news.tut.by/rss/index.rss";
                NewsRecipient = new NewsRecipient(url);
                if (await NewsRecipient.GetXmlNews())
                {
                    if (await NewsRecipient.ReadXml())
                    {
                        listNews.Items.Clear();
                        labelTime.Text = "Последнее обновление " + DateTime.Now.ToString();
                        int i = 0;
                        foreach (News article in NewsRecipient.articles)
                        {
                            ListViewItem item = new ListViewItem(new string[] { (++i).ToString(), article.date, article.title });
                            listNews.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    public class News
    {
        public string title;
        public string link;
        public string description;
        public string date;

        public News()
        {
            title = "";
            link = "";
            description = "";
            date = "";
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

        public Task<bool> GetXmlNews()
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
                return Task.Run(() => true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Task.Run(() => false);
            }
        }

        public Task<bool> ReadXml()
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
                                    article.title = item.InnerText;
                                }
                                else if (item.Name == "link")
                                {
                                    article.link = item.InnerText;
                                }
                                else if (item.Name == "description")
                                {
                                    article.description = item.InnerText;
                                }
                                else if (item.Name == "pubDate")
                                {
                                    article.date = item.InnerText;
                                }
                            }
                            articles.Add(article);
                        }
                    }
                }
                return Task.Run(() => true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return Task.Run(() => false);
            }
        }
    }
}
