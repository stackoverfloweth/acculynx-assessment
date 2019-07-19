<template>
  <div>
    <h1>{{ title }}</h1>
  </div>
</template>

<script>
  import api from '@/WebApiCommunications';

  export default {
    data() {
      return {
        title: 'Testing'
      }
    },
    //async created() {
    //  this.getAll()
    //},
    methods: {
      async getAll() {
        this.loading = true

        try {
          this.records = await api.getAll()
        } finally {
          this.loading = false
        }
      }
    }
  }
</script>

<style>
</style>
