using Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Api.Controllers {
    public class QuestionController : ApiController {
        private readonly IQuestionFetcher _questionFetcher;

        public QuestionController(IQuestionFetcher questionFetcher) {
            _questionFetcher = questionFetcher;
        }

        [HttpGet]
        public IEnumerable<string> Get() {
            var questions = _questionFetcher.FetchQuestions();
            return new string[] { "value1", "value2" };
        }
    }
}
