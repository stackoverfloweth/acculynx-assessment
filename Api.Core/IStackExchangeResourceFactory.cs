using System.Collections.Generic;
using Api.Contract.Enums;
using RestSharp;

namespace Api.Core {
    public interface IStackExchangeResourceFactory {
        string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters);
        string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum);
    }
}