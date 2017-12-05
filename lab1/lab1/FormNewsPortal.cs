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
        private static ServiceNewsClient client = new ServiceNewsClient();
        News[] listNewsRecieve;

        public FormNewsPortal()
        {
            InitializeComponent();
            listNews.FullRowSelect = true;
            Type type = listNews.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(listNews, true, null);

            GetNews(true);
        }
        private void timer_TickAsync(object sender, EventArgs e)
        {
            GetNews(true);
        }

        private void listNews_DoubleClick(object sender, EventArgs e)
        {
            Process.Start(listNewsRecieve[Int32.Parse(listNews.SelectedItems[0].Text)].LinkValue);
        }

        Func<string, bool, News[]> a = (url, serialize) =>
        {
            try
            {
                if (serialize)
                {
                    using (Stream stream = new MemoryStream())
                    {
                        byte[] data = System.Text.Encoding.UTF8.GetBytes(client.GetNewsSerialaized(url));
                        stream.Write(data, 0, data.Length);
                        stream.Position = 0;
                        var dataContractSerializer = new DataContractSerializer(typeof(News[]));
                        return (News[])dataContractSerializer.ReadObject(stream);
                    }
                }
                else
                {
                    return client.GetNews(url);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        };

        private async void GetNews(bool serialize)
        {
            try
            {
                string url = "https://news.tut.by/rss/index.rss";
                if ((listNewsRecieve = await Task.Factory.StartNew(() => a(url, serialize))) != null)
                {
                    labelTime.Text = "Последнее обновление " + DateTime.Now.ToString();
                    listNews.Items.Clear();
                }
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

        private void buttonSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (listNews.SelectedIndices.Count > 0)
                {
                    client.SendEmailAsync(listNewsRecieve[Int32.Parse(listNews.SelectedItems[0].Text)], textBoxEmail.Text);
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
