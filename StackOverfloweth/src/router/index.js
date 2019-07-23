import Vue from 'vue';
import Router from 'vue-router';
import QuestionList from '@/components/QuestionList';
import Question from '@/components/Question';
import ReviewList from '@/components/ReviewList';
import Review from '@/components/Review';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/',
      name: 'question-list',
      component: QuestionList
    },
    {
      path: '/review',
      name: 'review-list',
      component: ReviewList
    },
    {
      path: '/review/:question_id',
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
