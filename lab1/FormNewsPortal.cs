using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            getSerializedNews();
        }
        private void timer_TickAsync(object sender, EventArgs e)
        {
            getSerializedNews();
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
                    try
                    {
                        string url = "https://news.tut.by/rss/index.rss";
                        return client.getNews(url);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                };
                if((listNewsRecieve = await Task<WcfNewsService.News[]>.Factory.StartNew(a)) != null)
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

        private async void getSerializedNews()
        {
            try
            {
                Func<WcfNewsService.News[]> a = () =>
                {
                    try
                    {
                        string url = "https://news.tut.by/rss/index.rss";
                        using (Stream stream = new MemoryStream())
                        {
                            byte[] data = System.Text.Encoding.UTF8.GetBytes(client.getNewsSerialaized(url));
                            stream.Write(data, 0, data.Length);
                            stream.Position = 0;
                            var dataContractSerializer = new DataContractSerializer(typeof(WcfNewsService.News[]));
                            return (WcfNewsService.News[])dataContractSerializer.ReadObject(stream);
                        }
                    }
                    catch(Exception)
                    {
                        return null;
                    }
                };
                if((listNewsRecieve = await Task<WcfNewsService.News[]>.Factory.StartNew(a)) != null)
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
    }
}
