using System.Collections.Generic;
using System.Linq;
using Api.Contract;
using Api.Contract.Enums;
using RestSharp;

namespace Api.Core {
    public class StackExchangeClient : IStackExchangeClient {
        private readonly IStackExchangeResourceFactory _stackExchangeResourceFactory;
        private readonly IRestClient _restClient;
        private readonly string _filterId;
        private readonly string _site;

        public StackExchangeClient(IStackExchangeResourceFactory stackExchangeResourceFactory) {
            _stackExchangeResourceFactory = stackExchangeResourceFactory;
            _restClient = new RestClient("https://api.stackexchange.com/2.2");
            _filterId = GetFieldFilterId();
            _site = "stackoverflow";
        }

        public ItemResponseDto<QuestionDto> GetLatestQuestions(int page) {
            var resource = _stackExchangeResourceFactory.FetchResource(StackExchangeResourceEnum.GetQuestions);
            var request = new RestRequest(resource);
            request.AddParameter("filter", _filterId);
            request.AddParameter("site", _site);
            request.AddParameter("pagesize", 100);
            request.AddParameter("page", page);

            var response = _restClient.Execute<ItemResponseDto<QuestionDto>>(request);
            if (response?.Data == null || response.Data.QuotaRemaining == 0) {
                return null;
            }

            return response.Data;
        }

        public QuestionDto GetQuestion(int questionId) {
            var response = GetQuestions(new List<int> { questionId });
            var questionList = response?.Items.ToList();

            return questionList?[0];
        }

        public ItemResponseDto<QuestionDto> GetQuestions(List<int> ids) {
            var resource = _stackExchangeResourceFactory.FetchResource(StackExchangeResourceEnum.LookupQuestions, string.Join(";", ids));
            var request = new RestRequest(resource);
            request.AddParameter("filter", _filterId);
            request.AddParameter("site", _site);

            var response = _restClient.Execute<ItemResponseDto<QuestionDto>>(request);
            if (response?.Data == null || response.Data.QuotaRemaining == 0) {
                return null;
            }

            return response.Data;
        }

        public ItemResponseDto<AnswerDto> GetAnswers(int id) {
            var resource = _stackExchangeResourceFactory.FetchResource(StackExchangeResourceEnum.GetQuestionAnswers, id);
            var request = new RestRequest(resource);
            request.AddParameter("filter", _filterId);
            request.AddParameter("site", _site);

            var response = _restClient.Execute<ItemResponseDto<AnswerDto>>(request);
            if (response?.Data == null || response.Data.QuotaRemaining == 0) {
                return null;
            }

            return response.Data;
        }

        private string GetFieldFilterId() {
            var resource = _stackExchangeResourceFactory.FetchResource(StackExchangeResourceEnum.CreateFilter);
            var request = new RestRequest(resource);
            request.AddParameter("include", "question.accepted_answer_id;question.body;answer.body");

            var response = _restClient.Execute<ItemResponseDto<FilterDto>>(request);
            if (response?.Data == null || response.Data.QuotaRemaining == 0) {
                return null;
            }

            return response.Data.Items?.FirstOrDefault()?.Filter;
        }
    }
}