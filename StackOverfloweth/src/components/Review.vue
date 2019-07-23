<template>
  <div class="container">
    <div>
      <b-alert variant="success" show>Nice Job! You guessed the right answer!</b-alert>
      <b-alert variant="danger" show>Oops! That wasn't the right answer</b-alert>
      <div class="col">
        <h3>{{question.title}}</h3>
        <div v-html="question.body"></div>
        <div v-for="tag in question.tags" class="tag m-1">{{tag}}</div>
        <div class="answers">
          <div v-if="loading" class="text-center">
            <img src="../assets/loading.gif" />
          </div>
          <div v-if="!loading" class="text-center">
            <answer-item v-for="item in answers" v-bind:key="item.answer_id" :answer="item" :question="question"></answer-item>
          </div>
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
        question: {},
        answers: []
      }
    },
    components: {
      AnswerItem
    },
    mounted() {
      this.loadQuestion()
      this.loadAnswers()
    },
    methods: {
      async loadQuestion() {
        this.question = await Api.getQuestion(this.$route.params.question_id)
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

<style>
</style>
