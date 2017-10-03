﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfNewsService
{
    [ServiceContract]
    public interface IServiceNews
    {

        [OperationContract]
        List<News> getNews(string url);
    }

    [DataContract]
    public class News
    {
        private string title;
        private string link;
        private string description;
        private string date;

        [DataMember]
        public string TitleValue
        {
            get { return title; }
            set { title = value; }
        }

        [DataMember]
        public string LinkValue
        {
            get { return link; }
            set { link = value; }
        }

        [DataMember]
        public string DescriptionValue
        {
            get { return description; }
            set { description = value; }
        }

        [DataMember]
        public string DateValue
        {
            get { return date; }
            set { date = value; }
        }
    }
}
