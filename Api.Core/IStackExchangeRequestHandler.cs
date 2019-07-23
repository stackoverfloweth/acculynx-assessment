using System.Collections.Generic;
using Api.Contract;
using Api.Contract.Enums;

namespace Api.Core {
    public interface IStackExchangeRequestHandler {
        IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters, IDictionary<string, object> data) where T : IStackExchangeResponseType;

        IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, IDictionary<string, object> data) where T : IStackExchangeResponseType;
        IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters) where T : IStackExchangeResponseType;
        IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum) where T : IStackExchangeResponseType;
    }
}