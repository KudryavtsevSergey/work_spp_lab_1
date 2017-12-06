using lab1.ServiceNews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class Client : IClient
    {
        private IServiceNews client;
        private INews[] listNewsRecieve;
        private Uri uri;

        public Client(IServiceNews client, string uri)
        {
            this.client = client;
            this.uri = new Uri(uri);
        }

        public INews[] GetNews(bool serialize)
        {
            try
            {
                if (serialize)
                {
                    using (Stream stream = new MemoryStream())
                    {
                        string serializeNews = client.GetNewsSerialaized(uri);
                        byte[] data = Encoding.UTF8.GetBytes(serializeNews);
                        stream.Write(data, 0, data.Length);
                        stream.Position = 0;
                        var dataContractSerializer = new DataContractSerializer(typeof(News[]));
                        listNewsRecieve = (News[])dataContractSerializer.ReadObject(stream);
                        return listNewsRecieve;
                    }
                }
                else
                {
                    listNewsRecieve = client.GetNews(uri);
                    return listNewsRecieve;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public News GetArticle(int index)
        {
            return listNewsRecieve[index];
        }

        public void SendEmail(int index, string email)
        {
            News news = GetArticle(index);
            client.SendEmailAsync(news, email);
        }
    }
}
