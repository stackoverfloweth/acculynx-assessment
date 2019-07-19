﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Core;

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
