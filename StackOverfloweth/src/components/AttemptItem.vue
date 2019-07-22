<template>
  <div>
    <question-item :question="attempt.question"></question-item>
  </div>
</template>

<script>
  import QuestionItem from '@/components/QuestionItem';

  export default {
    components: {
      QuestionItem
    },
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
