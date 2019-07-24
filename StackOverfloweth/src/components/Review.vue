<template>
  <div class="container">
    <div class="row">
      <div class="col">
        <b-alert v-if="!loading && attemptedQuestion.attempt.answered_correctly" variant="success" show>Nice Job! You guessed the right answer!</b-alert>
        <b-alert v-if="!loading && !attemptedQuestion.attempt.answered_correctly" variant="danger" show>Oops! That wasn't the right answer</b-alert>
      </div>
    </div>
    <div class="row">
      <div class="col-md-6 question">
        <h3>{{attemptedQuestion.question.title}}</h3>
        <div v-html="attemptedQuestion.question.body"></div>
        <div v-for="tag in attemptedQuestion.question.tags" class="tag m-1">{{tag}}</div>
      </div>
      <div class="col-md-6 answers">
        <div v-if="loading" class="text-center">
          <img src="../assets/loading.gif" />
        </div>
        <div v-if="!loading">
          <answer-item v-for="answer in answers" v-bind:key="answer.answer_id" :attempt="answer.answer_id == attemptedQuestion.attempt.answer_id ? attemptedQuestion.attempt : {}" :answer="answer" :question="attemptedQuestion.question" :reviewMode="true"></answer-item>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import Api from '@/api';
  import AnswerItem from '@/components/AnswerItem';

  export default {
    data() {
      return {
        loading: true,
        attemptedQuestion: {
          question: {},
          attempt: {}
        },
        answers: []
      }
    },
    components: {
      AnswerItem
    },
    mounted() {
      window.scrollTo(0, 0);
      this.loadQuestion()
      this.loadAnswers()
    },
    methods: {
      async loadQuestion() {
        this.attemptedQuestion = await Api.getAttemptedQuestion(this.$route.params.question_id)
      },
      async loadAnswers() {
        this.loading = true

        try {
          this.answers = await Api.getAnswers(this.$route.params.question_id)
        } finally {
          this.loading = false
        }
      }
    }
  }
</script>

<style scoped>
  .card {
    cursor: default;
  }

  .card:hover {
    border-color: rgba(0, 0, 0, 0.125);
  }
</style>
