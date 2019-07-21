<template>
  <div id="app">
    <header class="mb-3">
      <b-navbar toggleable="md" type="light" variant="light">
        <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>
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
</style>
