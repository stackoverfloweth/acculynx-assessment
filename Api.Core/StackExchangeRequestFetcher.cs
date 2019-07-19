using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Contract.Enums;
using RestSharp;

namespace Api.Core {
    public class StackExchangeRequestBuilder : IStackExchangeRequestBuilder {
        private readonly IStackExchangeResourceFetcher _stackExchangeResourceFetcher;

        public StackExchangeRequestBuilder(IStackExchangeResourceFetcher stackExchangeResourceFetcher) {
            _stackExchangeResourceFetcher = stackExchangeResourceFetcher;
        }

        public IRestRequest BuildRequest(StackExchangeResourceEnum stackExchangeResourceEnum) {
            var resource = _stackExchangeResourceFetcher.FetchResource(StackExchangeResourceEnum.Question);
            var request = new RestRequest(resource);

            request.AddParameter("site", "stackoverflow");
            request.AddParameter("pagesize", 100);

            return request;
        }
    }
}
