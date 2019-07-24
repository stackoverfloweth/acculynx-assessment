<template>
  <b-card v-on:click="selectAnswer" v-bind:title="answer.title" v-bind:class="attempt.answer_id != null && 'answer-' + attempt.answered_correctly">
    <div v-if="reviewMode" slot="header">
      <div v-if="attempt.score != null">Your score {{attempt.score}}%</div>
      <div>Other users have guessed this answer <strong>{{answer.attempt_count || 0}}</strong> times</strong></div>
    </div>
    <div v-html="answer.body"></div>
  </b-card>
</template>

<script>
  import Api from '@/api';

  export default {
    props: {
      question: {
        type: Object,
        required: true,
        default: {}
      },
      answer: {
        type: Object,
        required: true,
        default: {}
      },
      attempt: {
        type: Object,
        required: false,
        default: () => ({
          answer_id: null,
          answered_correctly: null,
          score: null,
          attempt_count: null,
        })
      },
      reviewMode: {
        type: Boolean,
        required: false,
        default: false
      }
    },
    methods: {
      selectAnswer() {
        if (this.reviewMode) {
          return;
        }

        this.$parent.loading = true;
        Api.submitAttempt({
          answer_id: this.answer.answer_id,
          accepted_answer_id: this.question.accepted_answer_id,
          question_id: this.question.question_id
        }, (response) => {
          this.$parent.loading = false;
          this.$router.push({ name: 'review', params: { question_id: response.data.question_id } })
        })
      }
    }
  }
</script>

<style scope>
</style>
