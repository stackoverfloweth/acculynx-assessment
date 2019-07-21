<template>
  <b-card v-on:click="openQuestion" v-bind:title="question.title" class="mb-2">
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
      }
    },
    methods: {
      openQuestion: function () {
        this.$router.push({ name: 'question', params: { questionId: this.question.question_id } })
      }
    }
  }
</script>

<style scope>
</style>
