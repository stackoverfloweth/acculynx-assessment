import axios from 'axios'

const client = axios.create({
  baseURL: 'http://api.stackoverfloweth.local/',
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
  getLatest() {
    return this.execute('get', 'question/latest');
  },
  getPrevious() {
    return this.execute('get', 'question/previous');
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
  getAttempt(questionId) {
    return this.execute('get', `question/${questionId}/attempt`);
  },
  submitAttempt(attempt, callback) {
    return this.execute('post', 'attempt', attempt, callback);
  }
};
