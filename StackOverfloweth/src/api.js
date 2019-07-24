import axios from 'axios'

const client = axios.create({
  baseURL: 'http://stackoverfloweth.azurewebsites.net/',
  json: true
});

export default {
  execute(method, url, data, callback) {
    return client({
      method,
      url,
      data
    }).then(response => {
      if (typeof callback == "function") {
        callback(response)
      }

      return response.data;
    });
  },
  getLatestQuestions() {
    return this.execute('get', 'question/latest');
  },
  getQuestion(questionId) {
    return this.execute('get', `question/${questionId}`);
  },
  getAnswers(questionId) {
    return this.execute('get', `question/${questionId}/answers`);
  },
  getAttempts(questionId) {
    return this.execute('get', `question/${questionId}/attempts`);
  },
  getPreviousQuestions() {
    return this.execute('get', 'attempt/previous');
  },
  getAttemptedQuestion(questionId) {
    return this.execute('get', `attempt/${questionId}`);
  },
  submitAttempt(attempt, callback) {
    return this.execute('post', 'attempt', attempt, callback);
  }
};
