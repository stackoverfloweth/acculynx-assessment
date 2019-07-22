<template>
  <b-card v-on:click="selectAnswer" v-bind:title="answer.title" class="mb-2">
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
      }
    },
    methods: {
      selectAnswer() {
        Api.submitAttempt({
          answer_id: this.answer.answer_id,
          accepted_answer_id: this.question.accepted_answer_id,
          question_id: this.question.question_id
        })
      }
    }
  }
</script>

<style scope>
</style>
