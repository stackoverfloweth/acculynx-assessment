using Api.Core;
using System.Collections.Generic;
using System.Web.Http;
using Api.Contract;
using Data.Repositories;

namespace Api.Controllers {
    public class QuestionController : ApiController {
        private readonly IQuestionFetcher _questionFetcher;
        private readonly IAttemptRepository _attemptRepository;

        public QuestionController(IQuestionFetcher questionFetcher, IAttemptRepository attemptRepository)
        {
            _questionFetcher = questionFetcher;
            _attemptRepository = attemptRepository;
        }

        [HttpGet]
        public IEnumerable<QuestionDto> FetchQuestions()
        {
            var attempts = _attemptRepository.GetAttempts();
            //return _questionFetcher.FetchQuestions();
            return null;
        }
    }
}
