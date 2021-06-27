<template>
  <div id="nav">
    <div class="logo">
      <img src="@/assets/logo_white.png" />
    </div>
    <div class="menu-items">
      <MenuItem to="/" text="Home"/>
      <MenuItem to="/about" text="About"/>
    </div>
    <div class="login-container">
      <Login />
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";
import Login from "@/components/Login.vue";
import MenuItem from "@/components/Navbar/MenuItem.vue";

@Options({
  props: {
    title: String,
  },
  components: {
    MenuItem,
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


.menu-items {
  flex-grow: 1;
}

.logo {
  position: absolute;
  height: 100%;
  left: 30px;
  top: 0;
}

.logo img {
  height: 100%;
  width: auto;
}


</style>