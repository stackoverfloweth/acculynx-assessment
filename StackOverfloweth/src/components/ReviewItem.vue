<template>
  <b-card v-on:click="openQuestion" v-bind:title="question.title" v-bind:class="attempt.answer_id != null && 'answer-' + attempt.answered_correctly">
    <div slot="header">
      <div v-if="attempt.score != null">Your score {{attempt.score}}%</div>
    </div>
    <div v-html="question.body"></div>
    <div v-for="tag in question.tags" class="tag m-1">{{tag}}</div>
  </b-card>
</template>

<script>
  export default {
    props: {
      question: {
        type: Object,
        required: true,
        default: {}
      },
      attempt: {
        type: Object,
        required: true,
        default: {}
      }
    },
    methods: {
      openQuestion() {
        this.$router.push({ name: 'review', params: { question_id: this.question.question_id } })
      }
    }
  }
</script>

<style scope>
</style>
