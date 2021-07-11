<template>
  <div id="nav">
    <div class="logo">
      <img src="@/assets/logo_white.png" />
    </div>
    <div class="menu-items">
      <MenuItem to="/" text="Home" />
      <MenuItem to="/about" text="About" />
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
export default class Navbar extends Vue {
  offsetTop = 0;

  mounted() {
    this.offsetTop = this.$el.offsetTop;
    window.addEventListener("scroll", this.handleScroll);
  }

  deactivated() {
    window.removeEventListener("scroll", this.handleScroll);
  }

  handleScroll() {
    if (window.pageYOffset >= this.offsetTop) {
      this.$el.classList.add("sticky");
    } else {
      this.$el.classList.remove("sticky");
    }
  }
}
</script> 
<style scoped>
#nav {
  padding: var(--navbar-padding-vertical);
  height: calc(
    var(--navbar-height) + var(--navbar-padding-vertical) +
      var(--navbar-padding-vertical)
  );
  background-color: var(--var-main-color);
  display: flex;
  justify-content: center;
  position: fixed;
  top: 0;
  z-index: 10;
  width: calc(100% - 16px);
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

/* .sticky {
  position: fixed !important;
  width: calc(100% - 16px);
  top: 0;
} */
</style>