<template>
  <div>
    <div v-if="loading" class="text-center">
      <img src="../assets/loading.gif" />
    </div>
    <div v-if="!loading" class="container">
      <div v-if="questions.length === 0" class="text-center text-muted">
        That's not good.. I didn't find any questions
      </div>
      <div class="row">
        <div class="col">
          <question-item v-for="(item, index) in questions" v-bind:key="item.question_id" :question="item"></question-item>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import Api from '@/api';
  import QuestionItem from '@/components/QuestionItem';

  export default {
    data() {
      return {
        loading: true,
        questions: []
      }
    },
    components: {
      QuestionItem
    },
    mounted() {
      this.loadQuestions()
    },
    methods: {
      async loadQuestions() {
        this.loading = true

        try {
          this.questions = await Api.getLatest()
        } finally {
          this.loading = false
        }
      }
    }
  }
</script>

<style scope>
</style>
