<template>
  <div id="app">
    <header class="mb-3">
      <b-navbar toggleable="md" type="light" variant="light">
        <b-navbar-brand to="/">
          <img src="./assets/logo.png" width="40" height="40" alt="logo">
          stackoverfloweth
        </b-navbar-brand>

        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

        <b-collapse id="nav-collapse" is-nav>
          <b-navbar-nav>
            <b-nav-item to="/">Play</b-nav-item>
            <b-nav-item to="/Review">Review</b-nav-item>
          </b-navbar-nav>
          <b-navbar-nav class="ml-auto">
            <span class="text-muted">By Evan Sutherland</span>
          </b-navbar-nav>
        </b-collapse>
      </b-navbar>
    </header>
    <main>
      <router-view></router-view>
    </main>
  </div>
</template>

<script>
  export default {
    name: 'app'
  }
</script>

<style>
  .card {
    cursor: pointer;
    overflow: auto;
    margin-bottom: 20px;
  }

  .card:hover {
    border-color: darkgrey;
  }

  .question img {
    max-width: 100%;
    height: auto;
  }
  
  .answer-true .card-header {
    color: #155724;
    background-color: #d4edda;
    border-color: #c3e6cb;
  }

  .answer-false .card-header {
    color: #721c24;
    background-color: #f8d7da;
    border-color: #f5c6cb;
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
