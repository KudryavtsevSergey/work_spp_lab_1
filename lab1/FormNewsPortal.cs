using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace lab1
{
    public partial class FormNewsPortal : Form
    {
        WcfNewsService.ServiceNewsClient client = new WcfNewsService.ServiceNewsClient();
        WcfNewsService.News[] listNewsRecieve;

        public FormNewsPortal()
        {
            InitializeComponent();
            listNews.FullRowSelect = true;
            Type type = listNews.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(listNews, true, null);

            getNews();
        }
        //NewsRecipient NewsRecipient;

        private void timer_TickAsync(object sender, EventArgs e)
        {
            getNews();
        }

        private void listNews_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(listNewsRecieve[Int32.Parse(listNews.SelectedItems[0].Text)].LinkValue);
        }

        private async void getNews()
        {
            try
            {
                Func<WcfNewsService.News[]> a = () =>
                {
                    string url = "https://news.tut.by/rss/index.rss";
                    return client.getNews(url);
                };
                labelTime.Text = "Последнее обновление " + DateTime.Now.ToString();
                listNewsRecieve = await Task<WcfNewsService.News[]>.Factory.StartNew(a);
                for (int i = 0; i < listNewsRecieve.Length; i++)
                {
                    listNews.Items.Add(new ListViewItem(new string[] { (i + 1).ToString(), listNewsRecieve[i].DateValue, listNewsRecieve[i].TitleValue }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
