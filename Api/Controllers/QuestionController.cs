using Api.Core;
using System.Collections.Generic;
using System.Web.Http;
using Api.Contract;

namespace Api.Controllers {
    public class QuestionController : ApiController {
        private readonly IQuestionFetcher _questionFetcher;

        public QuestionController(IQuestionFetcher questionFetcher) {
            _questionFetcher = questionFetcher;
        }

        [HttpGet]
        public IEnumerable<QuestionDto> FetchQuestions() {
            return _questionFetcher.FetchQuestions();
        }
    }
}
