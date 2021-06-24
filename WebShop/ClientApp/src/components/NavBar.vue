<template>
  <div id="nav">
    <div class="logo">
      <img src="@/assets/logo_white.png" />
    </div>
    <div class="menu-items">
      <router-link to="/">Home</router-link>
      <router-link to="/about">About</router-link>
    </div>
    <div class="login-container">
      <Login />
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import Login from "@/components/Login.vue";

@Options({
  props: {
    title: String,
  },
  components: {
    Login,
  },
})
export default class Navbar extends Vue {}
</script> 
<style scoped>
#nav {
  padding: 30px;
  background-color: #025844;
  display: flex;
  justify-content: center;
  position: relative;
}

#nav a {
  color: #fff;
}

.menu-items {
  flex-grow: 1;
}

.logo {
  position: absolute;
  height: 100%;
  left: 30px;
  top: 0;
}

.logo img{
  height: 100%;
  width: auto;
}
.menu-items a.router-link-exact-active {
  /* color: #42b983; */
  text-decoration: none;
  border-bottom: 3px solid white;
  padding-bottom: 5px;
  border-color: #50e3c2;
  margin-right: 1rem;
}

.login-container {
}
</style>