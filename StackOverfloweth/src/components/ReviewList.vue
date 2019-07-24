<template>
  <div>
    <div v-if="loading" class="text-center">
      <img src="../assets/loading.gif" />
    </div>
    <div v-if="!loading" class="container">
      <div v-if="attempts.length === 0" class="text-center text-muted">
        Hmm... I don't have any record of attempts made from this user
      </div>
      <div class="row">
        <div class="col question">
          <review-item v-for="(item, index) in attempts" v-bind:key="item.question.question_id" :question="item.question" :attempt="item.attempt"></review-item>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import Api from '@/api';
  import ReviewItem from '@/components/ReviewItem';

  export default {
    data() {
      return {
        loading: true,
        attempts: []
      }
    },
    components: {
      ReviewItem
    },
    mounted() {
      this.loadAttempts()
    },
    methods: {
      async loadAttempts() {
        this.loading = true

        try {
          this.attempts = await Api.getPreviousQuestions()
        } finally {
          this.loading = false
        }
      }
    }
  }
</script>

<style>
</style>
