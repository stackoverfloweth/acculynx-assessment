using System.Collections.Generic;
using System.Linq;
using Api.Contract;
using Api.Contract.Enums;
using RestSharp;

namespace Api.Core {
    public class StackExchangeClient : IStackExchangeClient {
        private readonly RestClient _restClient;
        private readonly IStackExchangeRequestBuilder _stackExchangeRequestBuilder;

        public StackExchangeClient(IStackExchangeRequestBuilder stackExchangeRequestBuilder) {
            _stackExchangeRequestBuilder = stackExchangeRequestBuilder;
            _restClient = new RestClient("https://api.stackexchange.com/2.2");
        }

        public QuestionResponseDto GetLatestQuestions(int page) {
            var request = _stackExchangeRequestBuilder.BuildRequest(StackExchangeResourceEnum.Question);
            request.AddParameter("page", page);

            return _restClient.Execute<QuestionResponseDto>(request).Data;
        }

        public QuestionResponseDto GetQuestions(List<int> ids) {
            var request = _stackExchangeRequestBuilder.BuildRequest(StackExchangeResourceEnum.Question);
            request.Resource += $"/{string.Join(";", ids)}";

            return _restClient.Execute<QuestionResponseDto>(request).Data;
        }

        public QuestionResponseDto GetAnswers(int id) {
            var request = _stackExchangeRequestBuilder.BuildRequest(StackExchangeResourceEnum.Question);
            request.Resource += $"/{id}/answers";

            return _restClient.Execute<QuestionResponseDto>(request).Data;
        }
    }
}