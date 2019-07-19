using Api.Contract;
using Api.Contract.Enums;
using RestSharp;

namespace Api.Core {
    public class StackExchangeClient : IStackExchangeClient {
        private readonly RestClient _restClient;
        private readonly IStackExchangeResourceFetcher _stackExchangeResourceFetcher;

        public StackExchangeClient(IStackExchangeResourceFetcher stackExchangeResourceFetcher) {
            _restClient = new RestClient("https://api.stackexchange.com/2.2");
            _stackExchangeResourceFetcher = stackExchangeResourceFetcher;
        }

        public QuestionResponseDto Questions(int page)
        {
            var resource = _stackExchangeResourceFetcher.FetchResource(StackExchangeResourceEnum.Question);
            var request = new RestRequest(resource);
            request.AddParameter("page", page);

            return _restClient.Execute<QuestionResponseDto>(request).Data;
        }
    }
}