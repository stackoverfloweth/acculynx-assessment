using Api.Contract;
using Api.Contract.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core {
    public class StackExchangeClient : IStackExchangeClient {
        private readonly IStackExchangeRequestHandler _stackExchangeRequestHandler;

        public StackExchangeClient(IStackExchangeRequestHandler stackExchangeRequestHandler) {
            _stackExchangeRequestHandler = stackExchangeRequestHandler;
        }

        public IEnumerable<QuestionDto> GetLatestQuestions(int page) {
            var data = new Dictionary<string, object> {
                {"pagesize", 100},
                {"page", page}
            };
            return _stackExchangeRequestHandler.Execute<QuestionDto>(StackExchangeResourceEnum.GetQuestions, data);
        }

        public QuestionDto GetQuestion(int questionId) {
            var questionList = GetQuestions(new List<int> { questionId }).ToList();

            return questionList.FirstOrDefault();
        }

        public IEnumerable<QuestionDto> GetQuestions(List<int> questionIds) {
            var idString = string.Join(";", questionIds);
            var parameters = new List<object> { idString };

            return _stackExchangeRequestHandler.Execute<QuestionDto>(StackExchangeResourceEnum.LookupQuestions, parameters);
        }

        public IEnumerable<AnswerDto> GetAnswers(int questionId) {
            var parameters = new List<object> { questionId };

            return _stackExchangeRequestHandler.Execute<AnswerDto>(StackExchangeResourceEnum.GetQuestionAnswers, parameters);
        }
    }
}