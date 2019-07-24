using Api.Contract.Enums;
using System.Collections.Generic;

namespace Api.Core {
    public interface IStackExchangeResourceFactory {
        string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters);
        string FetchResource(StackExchangeResourceEnum stackExchangeResourceEnum);
    }
}