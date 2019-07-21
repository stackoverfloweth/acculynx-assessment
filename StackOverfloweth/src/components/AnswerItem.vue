<template>
  <b-card v-on:click="openQuestion" v-bind:title="question.Title" class="mb-2">
    <div v-html="question.Body"></div>
    <div v-for="tag in question.Tags" class="tag m-1">{{tag}}</div>
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
        console.log(this.question);
      }
    }
  }
</script>

<style scope>
  .card {
    cursor: pointer;
  }

  pre {
    margin-bottom: 1em;
    padding: 12px 8px;
    max-height: 600px;
    overflow: auto;
    font-family: Consolas,Menlo,Monaco,Lucida Console,Liberation Mono,DejaVu Sans Mono,Bitstream Vera Sans Mono,Courier New,monospace,sans-serif;
    font-size: 13px;
    background-color: #eff0f1;
    border-radius: 3px;
  }

  .tag {
    font-size: 12px;
    line-height: 12px;
    padding: .4em .5em;
    display: inline-block;
    color: #39739d;
    background-color: #E1ECF4;
    border-color: #E1ECF4;
  }
</style>
