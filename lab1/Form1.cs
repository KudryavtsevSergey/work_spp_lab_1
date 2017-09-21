using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
        }

        List<Data> dataList = new List<Data>();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "https://www.tut.by/";
                HtmlParser parser = new HtmlParser(url);
                dataList = parser.getData();
                foreach (Data data in dataList)
                {
                    ListViewItem item = new ListViewItem(new string[] { data.id.ToString(), data.date, data.name});
                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(dataList[Int32.Parse(listView1.SelectedItems[0].Text)].link);
        }
    }

    public struct Data
    {
        public int id;
        public string date;
        public string name;
        public string link;
    }

    public class HtmlParser
    {
        private List<Data> dataList = new List<Data>();

        private string url;
        private string html;

        public HtmlParser()
        {
            this.url = "https://www.tut.by/";
        }

        public HtmlParser(string url)
        {
            this.url = url;
        }

        public List<Data> getData()
        {
            Data item;

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            StreamReader myStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream());
            html = myStreamReader.ReadToEnd();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode bodyNode = doc.DocumentNode.SelectSingleNode("//div[@id='latest']");
            doc.LoadHtml(bodyNode.InnerHtml);
            HtmlNodeCollection collection = doc.DocumentNode.SelectNodes("//a[@class='entry__link io-block-link']");

            if (collection != null)
            {
                int id = 0;
                foreach (HtmlNode node in collection)
                {
                    id++;
                    string pattern = "href=\"(.*)\" class";
                    Match match = Regex.Match(node.OuterHtml, pattern);
                    item.id = id;
                    item.link = match.Groups[1].ToString();
                    item.name = node.ChildNodes[0].ChildNodes[1].InnerText;
                    item.date = node.ChildNodes[0].ChildNodes[0].ChildNodes[0].InnerText.Replace("&nbsp;", "");
                    dataList.Add(item);
                }
            }
            return dataList;
        }
    }
}
