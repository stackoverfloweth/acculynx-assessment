using System.Collections.Generic;
using System.Linq;
using Api.Contract;
using Api.Contract.Enums;
using RestSharp;

namespace Api.Core {
    public class StackExchangeRequestHandler : IStackExchangeRequestHandler {
        private readonly IStackExchangeResourceFactory _stackExchangeResourceFactory;
        private readonly IStackExchangeFilterCreator _stackExchangeFilterCreator;
        private readonly string _url;
        private readonly string _key;
        private readonly string _site;

        public StackExchangeRequestHandler(IStackExchangeResourceFactory stackExchangeResourceFactory, IStackExchangeFilterCreator stackExchangeFilterCreator) {
            _stackExchangeResourceFactory = stackExchangeResourceFactory;
            _stackExchangeFilterCreator = stackExchangeFilterCreator;
            _url = "https://api.stackexchange.com/2.2";
            _key = "cXOea6bOwSD2EuIw7XPIlA((";
            _site = "stackoverflow";
        }

        public IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters, IDictionary<string, object> data) {
            var resource = _stackExchangeResourceFactory.FetchResource(stackExchangeResourceEnum, parameters);
            var client = new RestClient(_url);
            var request = new RestRequest(resource);
            var filter = _stackExchangeFilterCreator.CreateFilter(client);

            request.AddParameter("filter", filter);
            request.AddParameter("key", _key);
            request.AddParameter("site", _site);

            foreach (var dataItem in data) {
                request.AddParameter(dataItem.Key, dataItem.Value);
            }

            var response = client.Execute<ItemResponseDto<T>>(request);
            if (response?.Data == null || response.Data.QuotaRemaining == 0) {
                return null;
            }

            return response.Data.Items;
        }

        public IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, IDictionary<string, object> data) {
            return Execute<T>(stackExchangeResourceEnum, null, data);
        }

        public IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters) {
            return Execute<T>(stackExchangeResourceEnum, parameters, null);
        }

        public IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum) {
            return Execute<T>(stackExchangeResourceEnum, null, null);
        }
    }
}
