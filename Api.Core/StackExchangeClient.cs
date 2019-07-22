using System.Collections.Generic;
using System.Linq;
using Api.Contract;
using Api.Contract.Enums;
using RestSharp;

namespace Api.Core {
    public class StackExchangeClient : IStackExchangeClient {
        private readonly IStackExchangeResourceFetcher _stackExchangeResourceFetcher;
        private readonly IRestClient _restClient;
        private readonly string _filterId;
        private readonly string _site;

        public StackExchangeClient(IStackExchangeResourceFetcher stackExchangeResourceFetcher) {
            _stackExchangeResourceFetcher = stackExchangeResourceFetcher;
            _restClient = new RestClient("https://api.stackexchange.com/2.2");
            _filterId = GetFieldFilterId();
            _site = "stackoverflow";
        }

        public ItemResponseDto<QuestionDto> GetLatestQuestions(int page) {
            var resource = _stackExchangeResourceFetcher.FetchResource(StackExchangeResourceEnum.GetQuestions);
            var request = new RestRequest(resource);
            request.AddParameter("filter", _filterId);
            request.AddParameter("site", _site);
            request.AddParameter("pagesize", 100);
            request.AddParameter("page", page);

            return _restClient.Execute<ItemResponseDto<QuestionDto>>(request)?.Data;
        }

        public QuestionDto GetQuestion(int questionId) {
            var response = GetQuestions(new List<int> { questionId });
            var questionList = response?.Items?.ToList();

            return questionList?[0];
        }

        public ItemResponseDto<QuestionDto> GetQuestions(List<int> ids) {
            var resource = _stackExchangeResourceFetcher.FetchResource(StackExchangeResourceEnum.LookupQuestions, string.Join(";", ids));
            var request = new RestRequest(resource);
            request.AddParameter("filter", _filterId);
            request.AddParameter("site", _site);

            return _restClient.Execute<ItemResponseDto<QuestionDto>>(request)?.Data;
        }

        public ItemResponseDto<AnswerDto> GetAnswers(int id) {
            var resource = _stackExchangeResourceFetcher.FetchResource(StackExchangeResourceEnum.GetQuestionAnswers, id);
            var request = new RestRequest(resource);
            request.AddParameter("filter", _filterId);
            request.AddParameter("site", _site);

            return _restClient.Execute<ItemResponseDto<AnswerDto>>(request).Data;
        }

        private string GetFieldFilterId() {
            var resource = _stackExchangeResourceFetcher.FetchResource(StackExchangeResourceEnum.CreateFilter);
            var request = new RestRequest(resource);
            request.AddParameter("include", "question.accepted_answer_id;question.body;answer.body");

            var response = _restClient.Execute<ItemResponseDto<FilterDto>>(request);

            return response?.Data?.Items?.FirstOrDefault()?.Filter;
        }
    }
}