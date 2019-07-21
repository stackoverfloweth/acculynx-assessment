<template>
  <div>
    <div v-if="loading" class="text-center">
      <img src="../assets/loading.gif" />
    </div>
    <div v-if="!loading" class="container">
      <div class="row">
        <div class="col">
          <attempt-item v-for="(item, index) in attempts" v-bind:key="item.question.question_id" :attempt="item"></attempt-item>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import Api from '@/api';
  import AttemptItem from '@/components/AttemptItem';

  export default {
    data() {
      return {
        loading: true,
        attempts: []
      }
    },
    components: {
      AttemptItem
    },
    mounted() {
      this.loadAttempts()
    },
    methods: {
      async loadAttempts() {
        this.loading = true

        try {
          this.attempts = await Api.getPrevious()
        } finally {
          this.loading = false
        }
      }
    }
  }
</script>

<style>
</style>
