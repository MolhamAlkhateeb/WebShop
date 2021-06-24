<template>
  <div class="login-container">
    <router-link to="/">Log in</router-link>
    <router-link to="/">Sign up</router-link>
  </div>
</template>

<style scoped>
.login-container {
  display: flex;
  margin: -1rem 0;
}

.login-container a {
  margin-right: 1rem;
  text-decoration: none;
  color: white;
  border: 2px solid white;
  padding: 1rem;
  border-radius: 3px;
}
</style>