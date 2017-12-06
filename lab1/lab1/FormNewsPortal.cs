using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab1.ServiceNews;

namespace lab1
{
    public partial class FormNewsPortal : Form
    {
        IClient client;

        public FormNewsPortal()
        {
            InitializeComponent();
            listNews.FullRowSelect = true;
            Type type = listNews.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(listNews, true, null);
            client = new Client("https://news.tut.by/rss/index.rss");
            GetNews(true);
        }
        private void timer_TickAsync(object sender, EventArgs e)
        {
            GetNews(true);
        }

        private async void GetNews(bool serialize)
        {
            Func<bool, News[]> a = (serializeFlag) =>
            {
                try
                {
                    return client.GetNews(serializeFlag);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            };
            News[] listNewsRecieve = await Task.Factory.StartNew(() => a(serialize));
            if (listNewsRecieve!= null)
            {
                labelTime.Text = "Последнее обновление " + DateTime.Now.ToString();
                listNews.Items.Clear();
                for (int i = 0; i < listNewsRecieve.Length; i++)
                {
                    listNews.Items.Add(new ListViewItem(new string[] { (i + 1).ToString(), listNewsRecieve[i].DateValue, listNewsRecieve[i].TitleValue }));
                }
            }
        }

        private void listNews_DoubleClick(object sender, EventArgs e)
        {
            int index = Int32.Parse(listNews.SelectedItems[0].Text);
            News news = client.GetArticle(index);
            Process.Start(news.LinkValue);
        }

        private void buttonSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (listNews.SelectedIndices.Count > 0)
                {
                    News news = client.GetArticle(Int32.Parse(listNews.SelectedItems[0].Text));
                    int index = Int32.Parse(listNews.SelectedItems[0].Text);
                    client.SendEmail(index, textBoxEmail.Text);
                }
                else
                {
                    MessageBox.Show("Не выделены элементы!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormNewsPortal_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Close();
            timer.Stop();
        }
    }
}
