import Vue from 'vue';
import Router from 'vue-router';
import Primary from '@/components/Primary';

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Primary',
      component: Primary
    },
    {
      path: '/Review',
      name: 'Review',
      component: Primary
    }
  ]
});
