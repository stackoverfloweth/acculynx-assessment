using Api.Contract;
using Api.Contract.Enums;
using RestSharp;
using System.Linq;

namespace Api.Core {
    public class StackExchangeFilterCreator : IStackExchangeFilterCreator {
        private readonly IStackExchangeResourceFactory _stackExchangeResourceFactory;

        public StackExchangeFilterCreator(IStackExchangeResourceFactory stackExchangeResourceFactory) {
            _stackExchangeResourceFactory = stackExchangeResourceFactory;
        }

        public string CreateFilter(IRestClient restClient) {
            var resource = _stackExchangeResourceFactory.FetchResource(StackExchangeResourceEnum.CreateFilter);
            var request = new RestRequest(resource);
            request.AddParameter("include", "question.accepted_answer_id;question.body;answer.body");

            var response = restClient.Execute<ItemResponseDto<FilterDto>>(request);
            if (response?.Data == null || response.Data.QuotaRemaining == 0) {
                return null;
            }

            return response.Data.Items?.FirstOrDefault()?.Filter;
        }
    }
}
