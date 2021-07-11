<template>
  <Navbar />
  <router-view />
  <Footer />
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Footer from "@/components/Footer/Footer.vue";
import Navbar from "@/components/Navbar/NavBar.vue";

@Options({
  components: {
    Footer,
    Navbar,
  },
})
export default class App extends Vue {
  created() {
    document.title = "Dwells";
  }
}
</script>
<style>
:root {
  --var-main-color: #025844;
  --var-secondary-color: #fff;
  --navbar-height: 33px;
  --navbar-padding-vertical: 30px;
}
#app {
  font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica,
    Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol";
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}
body {
  margin-top: calc(
    var(--navbar-height) + var(--navbar-padding-vertical) +
      var(--navbar-padding-vertical)
  );
}

.block {
  display: block;
}
.block-inline {
  display: inline-block;
}

.font-size-1-em {
  font-size: 1em !important;
}

.full-width {
  width: 100%;
}

.hover-reverse:hover {
  color: #000;
}

.btn {
  text-decoration: none;
  cursor: pointer;
  background-color: #fff;
  color: var(--var-main-color);
  border: 0.125em solid var(--var-main-color);
  padding: 0.25em 1em;
  border-radius: 0.25em;
  transition: color 0.1s linear, border-color 0.2s linear,
    background-color 0.2s linear;
}

.btn:hover {
  color: #fff;
  border-color: var(--var-main-color);
  background-color: var(--var-main-color);
}
.btn-reverse {
  text-decoration: none;
  cursor: pointer;
  color: var(--var-secondary-color);
  border: 0.125em solid var(--var-secondary-color);
  padding: 0.25em 1em;
  border-radius: 0.25em;
  transition: color 0.1s linear, border-color 0.2s linear,
    background-color 0.2s linear;
}

.btn-reverse:hover {
  color: #000;
  border-color: #000;
}

@font-face {
  font-family: "dwells-medium";
  src: url("assets/fonts/dwells-medium.woff2") format("woff2");
}
</style>
