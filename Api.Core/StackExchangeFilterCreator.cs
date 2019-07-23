using Api.Contract;
using Api.Contract.Enums;
using RestSharp;
using System.Linq;

namespace Api.Core {
    public class StackExchangeFilterCreator : IStackExchangeFilterCreator {
        private readonly IStackExchangeResourceFactory _stackExchangeResourceFactory;
        private readonly IRestSharpWrapper _restSharpWrapper;
        private readonly string _url;

        public StackExchangeFilterCreator(IStackExchangeResourceFactory stackExchangeResourceFactory, IRestSharpWrapper restSharpWrapper) {
            _stackExchangeResourceFactory = stackExchangeResourceFactory;
            _restSharpWrapper = restSharpWrapper;
            _url = "https://api.stackexchange.com/2.2";
        }

        public string CreateFilter() {
            var resource = _stackExchangeResourceFactory.FetchResource(StackExchangeResourceEnum.CreateFilter);
            var request = _restSharpWrapper.CreateRestRequest(resource);
            request.AddParameter("include", "question.accepted_answer_id;question.body;answer.body");

            var client = _restSharpWrapper.CreateRestClient(_url);
            var response = client.Execute<ItemResponseDto<FilterDto>>(request);
            if (response?.Data == null || response.Data.QuotaRemaining == 0) {
                return null;
            }

            return response.Data.Items?.FirstOrDefault()?.Filter;
        }
    }
}
