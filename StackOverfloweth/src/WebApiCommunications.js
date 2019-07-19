import Vue from 'vue'
import axios from 'axios'

const client = axios.create({
  baseURL: 'http://api.stackoverfloweth.local/api',
  json: true
});

export default {
  execute(method, url, data) {
    return client({
      method,
      url,
      data
    }).then(request => {
      return request.data;
    });
  },
  getAll() {
    return this.execute('get', 'question');
  }
};
