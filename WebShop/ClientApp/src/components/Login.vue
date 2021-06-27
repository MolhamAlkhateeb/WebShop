<template>
  <div class="login-container">
    <router-link to="/">Log in</router-link>
    <router-link to="/">Sign up</router-link>
  </div>
</template>

<style scoped>
.login-container a {
  text-decoration: none;
  cursor: pointer;
  color: #fff;
  border: 0.125em solid #fff;
  padding: 0.25em 1em;
  border-radius: 0.25em;
  transition: color 0.1s linear, border-color 0.2s linear;
}

.login-container a:not(:last-child) {
  margin-right: 1em;
}

.login-container a:hover {
  color: #000;
  border-color: #000;
}
</style>