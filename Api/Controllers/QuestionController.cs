using Api.Core;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using Api.Contract;

namespace Api.Controllers {
    public class QuestionController : ApiController {
        private readonly IFilteredLatestQuestionsFetcher _filteredLatestQuestionsFetcher;
        private readonly IPreviouslyAttemptedQuestionFetcher _previouslyAttemptedQuestionFetcher;

        public QuestionController(IFilteredLatestQuestionsFetcher filteredLatestQuestionsFetcher, IPreviouslyAttemptedQuestionFetcher previouslyAttemptedQuestionFetcher) {
            _filteredLatestQuestionsFetcher = filteredLatestQuestionsFetcher;
            _previouslyAttemptedQuestionFetcher = previouslyAttemptedQuestionFetcher;
        }

        [HttpGet]
        [Route("Latest")]
        public IEnumerable<QuestionDto> FetchLatestQuestions() {
            var latestQuestions = _filteredLatestQuestionsFetcher.FetchQuestions();

            return latestQuestions;
        }

        [HttpGet]
        [Route("Previous")]
        public IEnumerable<QuestionDto> FetchPreviousQuestions() {
            var ip = HttpContext.Current.Request.UserHostAddress;
            var previousQuestions = _previouslyAttemptedQuestionFetcher.FetchQuestions(ip);

            return previousQuestions;
        }
    }
}
