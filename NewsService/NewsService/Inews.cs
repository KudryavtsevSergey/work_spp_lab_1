
using System.ServiceModel;

namespace NewsService
{
    [ServiceContract]
    [ServiceKnownType(typeof(News))]
    public interface INews
    {
        string TitleValue
        {
            [OperationContract]
            get;
            [OperationContract]
            set;
        }

        string LinkValue
        {
            [OperationContract]
            get;
            [OperationContract]
            set;
        }

        string DescriptionValue
        {
            [OperationContract]
            get;
            [OperationContract]
            set;
        }

        string DateValue
        {
            [OperationContract]
            get;
            [OperationContract]
            set;
        }
    }
}