using Api.Contract.Enums;
using RestSharp;

namespace Api.Core
{
    public interface IStackExchangeResourceFactory
    {
        string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum, object argument);
        string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum);
    }
}