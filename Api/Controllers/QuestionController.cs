using Api.Contract;
using Api.Core;
using System.Collections.Generic;
using System.Web.Http;

namespace Api.Controllers {
    [RoutePrefix("question")]
    public class QuestionController : ApiController {
        private readonly IFilteredLatestQuestionsFetcher _filteredLatestQuestionsFetcher;
        private readonly IPreviouslyAttemptedQuestionFetcher _previouslyAttemptedQuestionFetcher;

        public QuestionController(IFilteredLatestQuestionsFetcher filteredLatestQuestionsFetcher, IPreviouslyAttemptedQuestionFetcher previouslyAttemptedQuestionFetcher) {
            _filteredLatestQuestionsFetcher = filteredLatestQuestionsFetcher;
            _previouslyAttemptedQuestionFetcher = previouslyAttemptedQuestionFetcher;
        }

        [HttpGet]
        [Route("latest")]
        public IEnumerable<QuestionDto> FetchLatestQuestions() {
            var latestQuestions = _filteredLatestQuestionsFetcher.FetchQuestions();

            return latestQuestions;
        }

        //[HttpGet]
        //[Route("previous")]
        //public IEnumerable<AttemptedQuestionDto> FetchPreviousQuestions() {
        //    var ip = HttpContext.Current.Request.UserHostAddress;
        //    var previousQuestions = _previouslyAttemptedQuestionFetcher.FetchQuestions(ip);

        //    return previousQuestions;
        //}

        //[HttpGet]
        //[Route("{id}/answers")]
        //public IEnumerable<QuestionDto> FetchAnswersForQuestion([FromUri] int questionId) {
        //    return null;
        //}
    }
}
