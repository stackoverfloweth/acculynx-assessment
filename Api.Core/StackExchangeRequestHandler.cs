using Api.Contract;
using Api.Contract.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core {
    public class StackExchangeRequestHandler : IStackExchangeRequestHandler {
        private readonly IStackExchangeResourceFactory _stackExchangeResourceFactory;
        private readonly IStackExchangeFilterCreator _stackExchangeFilterCreator;
        private readonly IRestSharpWrapper _restSharpWrapper;
        private readonly string _url;
        private readonly string _key;
        private readonly string _site;

        public StackExchangeRequestHandler(IStackExchangeResourceFactory stackExchangeResourceFactory, IStackExchangeFilterCreator stackExchangeFilterCreator, IRestSharpWrapper restSharpWrapper) {
            _stackExchangeResourceFactory = stackExchangeResourceFactory;
            _stackExchangeFilterCreator = stackExchangeFilterCreator;
            _restSharpWrapper = restSharpWrapper;
            _url = "https://api.stackexchange.com/2.2";
            _key = "cXOea6bOwSD2EuIw7XPIlA((";
            _site = "stackoverflow";
        }

        public IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, List<object> parameters, Dictionary<string, object> data) {
            var resource = _stackExchangeResourceFactory.FetchResource(stackExchangeResourceEnum, parameters);
            var client = _restSharpWrapper.CreateRestClient(_url);
            var request = _restSharpWrapper.CreateRestRequest(resource);
            var filter = _stackExchangeFilterCreator.CreateFilter();

            request.AddParameter("filter", filter);
            request.AddParameter("key", _key);
            request.AddParameter("site", _site);

            if (data != null && data.Keys.Any()) {
                foreach (var dataItem in data) {
                    request.AddParameter(dataItem.Key, dataItem.Value);
                }
            }

            var response = client.Execute<ItemResponseDto<T>>(request);
            if (response?.Data == null || response.Data.QuotaRemaining == 0) {
                return null;
            }

            return response.Data.Items;
        }

        public IEnumerable<T> Execute<T>(StackExchangeResourceEnum stackExchangeResourceEnum, Dictionary<string, object> data) {
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
