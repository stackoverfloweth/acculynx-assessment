using Api.Contract.Enums;
using RestSharp;

namespace Api.Core
{
    public interface IStackExchangeRequestBuilder
    {
        IRestRequest BuildRequest(StackExchangeResourceEnum stackExchangeResourceEnum);
    }
}