﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace lab1.WcfNewsService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="News", Namespace="http://schemas.datacontract.org/2004/07/WcfNewsService")]
    [System.SerializableAttribute()]
    public partial class News : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DateValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LinkValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DateValue {
            get {
                return this.DateValueField;
            }
            set {
                if ((object.ReferenceEquals(this.DateValueField, value) != true)) {
                    this.DateValueField = value;
                    this.RaisePropertyChanged("DateValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DescriptionValue {
            get {
                return this.DescriptionValueField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionValueField, value) != true)) {
                    this.DescriptionValueField = value;
                    this.RaisePropertyChanged("DescriptionValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LinkValue {
            get {
                return this.LinkValueField;
            }
            set {
                if ((object.ReferenceEquals(this.LinkValueField, value) != true)) {
                    this.LinkValueField = value;
                    this.RaisePropertyChanged("LinkValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TitleValue {
            get {
                return this.TitleValueField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleValueField, value) != true)) {
                    this.TitleValueField = value;
                    this.RaisePropertyChanged("TitleValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfNewsService.IServiceNews")]
    public interface IServiceNews {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceNews/getNews", ReplyAction="http://tempuri.org/IServiceNews/getNewsResponse")]
        lab1.WcfNewsService.News[] getNews(string url);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceNews/getNews", ReplyAction="http://tempuri.org/IServiceNews/getNewsResponse")]
        System.Threading.Tasks.Task<lab1.WcfNewsService.News[]> getNewsAsync(string url);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceNews/getNewsSerialaized", ReplyAction="http://tempuri.org/IServiceNews/getNewsSerialaizedResponse")]
        string getNewsSerialaized(string url);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceNews/getNewsSerialaized", ReplyAction="http://tempuri.org/IServiceNews/getNewsSerialaizedResponse")]
        System.Threading.Tasks.Task<string> getNewsSerialaizedAsync(string url);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceNews/SendEmailAsync", ReplyAction="http://tempuri.org/IServiceNews/SendEmailAsyncResponse")]
        void SendEmailAsync(lab1.WcfNewsService.News article, string receiversAddress);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceNews/SendEmailAsync", ReplyAction="http://tempuri.org/IServiceNews/SendEmailAsyncResponse")]
        System.Threading.Tasks.Task SendEmailAsyncAsync(lab1.WcfNewsService.News article, string receiversAddress);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceNewsChannel : lab1.WcfNewsService.IServiceNews, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceNewsClient : System.ServiceModel.ClientBase<lab1.WcfNewsService.IServiceNews>, lab1.WcfNewsService.IServiceNews {
        
        public ServiceNewsClient() {
        }
        
        public ServiceNewsClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceNewsClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceNewsClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceNewsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public lab1.WcfNewsService.News[] getNews(string url) {
            return base.Channel.getNews(url);
        }
        
        public System.Threading.Tasks.Task<lab1.WcfNewsService.News[]> getNewsAsync(string url) {
            return base.Channel.getNewsAsync(url);
        }
        
        public string getNewsSerialaized(string url) {
            return base.Channel.getNewsSerialaized(url);
        }
        
        public System.Threading.Tasks.Task<string> getNewsSerialaizedAsync(string url) {
            return base.Channel.getNewsSerialaizedAsync(url);
        }
        
        public void SendEmailAsync(lab1.WcfNewsService.News article, string receiversAddress) {
            base.Channel.SendEmailAsync(article, receiversAddress);
        }
        
        public System.Threading.Tasks.Task SendEmailAsyncAsync(lab1.WcfNewsService.News article, string receiversAddress) {
            return base.Channel.SendEmailAsyncAsync(article, receiversAddress);
        }
    }
}