<template>
  <Navbar />
  <router-view />
  <Footer />
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Footer from "@/components/Footer/Footer.vue";
import Navbar from "@/components/NavBar.vue";

@Options({
  components: {
    Footer,
    Navbar,
  },
})
export default class App extends Vue {}
</script>
<style>
#app {
  font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol";
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
}

@font-face {
  font-family: "dwells-medium";
  src: url("assets/fonts/dwells-medium.woff2") format("woff2");
}

</style>
