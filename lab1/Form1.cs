using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
        }
        NewsRecipient NewsRecipient;

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(NewsRecipient.articles[Int32.Parse(listView1.SelectedItems[0].Text)].link);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://news.tut.by/rss/index.rss";
                NewsRecipient = new NewsRecipient(url);
                NewsRecipient.GetXmlNews();
                NewsRecipient.ReadXml();
                int i = 0;
                foreach (News article in NewsRecipient.articles)
                {
                    ListViewItem item = new ListViewItem(new string[] { (i++).ToString(), article.date, article.title });
                    listView1.Items.Add(item);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
    }
}
