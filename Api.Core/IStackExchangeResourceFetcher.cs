using Api.Contract.Enums;

namespace Api.Core
{
    public interface IStackExchangeResourceFetcher
    {
        string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum);
    }
}