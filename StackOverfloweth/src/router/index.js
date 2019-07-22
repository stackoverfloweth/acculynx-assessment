import Vue from 'vue';
import Router from 'vue-router';
import Primary from '@/components/Primary';
import Review from '@/components/Review';
import Question from '@/components/Question';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/',
      name: 'primary',
      component: Primary
    },
    {
      path: '/review',
      name: 'review',
      component: Review
    },
    {
      path: '/question/:question_id',
      name: 'question',
      component: Question
    }
  ]
});
