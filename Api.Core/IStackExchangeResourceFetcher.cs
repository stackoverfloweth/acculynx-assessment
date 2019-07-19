using Api.Contract.Enums;
using RestSharp;

namespace Api.Core
{
    public interface IStackExchangeResourceFetcher
    {
        string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum);
    }
}