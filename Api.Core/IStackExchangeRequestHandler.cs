using Api.Contract.Enums;
using System.Collections.Generic;

namespace Api.Core {
    public interface IStackExchangeRequestHandler {
        IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters, IDictionary<string, object> data) ;
        IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, IDictionary<string, object> data) ;
        IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters) ;
        IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum) ;
    }
}