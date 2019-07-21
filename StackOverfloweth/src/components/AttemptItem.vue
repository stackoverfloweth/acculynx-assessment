<template>
  <b-card v-bind:title="attempt.question.title" class="mb-2">
    <div v-html="attempt.question.body"></div>
    <div v-for="tag in attempt.question.tags" class="tag m-1">{{tag}}</div>
  </b-card>
</template>

<script>
  export default {
    props: {
      attempt: {
        type: Object,
        required: true,
        default: {}
      }
    }
  }
</script>

<style scope>
</style>
