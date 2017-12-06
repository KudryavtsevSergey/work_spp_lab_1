using lab1.ServiceNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    interface IClient
    {
        News[] GetNews(bool serialize);
        News GetArticle(int index);
        void SendEmail(int index, string email);
        void Close();
    }
}
