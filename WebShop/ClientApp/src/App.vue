<template>
  <Navbar />
  <router-view v-slot="{ Component }">
    <transition name="slide" mode="out-in">
      <component :is="Component"></component>
    </transition>
  </router-view>
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
  overflow: hidden;
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
  text-align: center;
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

.shake {
  animation: shake 0.5s;
}

.slide-enter-from {
  opacity: 0;
  transform: translateX(2em);
}

.slide-enter-active {
  transition: all 0.3s ease-out;
}

.slide-leave-to {
  opacity: 0;
  transform: translateX(-2em);
}
.slide-leave-active {
  transition: all 0.3s ease-in;
}

.pop-leave-to,
.pop-enter-from {
  opacity: 0;
  max-height: 50vh;
  transform: scale(50%);
}

.pop-leave-active {
  transition: all 0.2s ease;
}
.pop-enter-active {
  transition: all 0.2s ease;
}

@font-face {
  font-family: "dwells-medium";
  src: url("assets/fonts/dwells-medium.woff2") format("woff2");
}

@keyframes shake {
  0% {
    transform: translate(1px, 1px) rotate(0deg);
  }
  10% {
    transform: translate(-1px, -2px) rotate(-1deg);
  }
  20% {
    transform: translate(-3px, 0px) rotate(1deg);
  }
  30% {
    transform: translate(3px, 2px) rotate(0deg);
  }
  40% {
    transform: translate(1px, -1px) rotate(1deg);
  }
  50% {
    transform: translate(-1px, 2px) rotate(-1deg);
  }
  60% {
    transform: translate(-3px, 1px) rotate(0deg);
  }
  70% {
    transform: translate(3px, 1px) rotate(-1deg);
  }
  80% {
    transform: translate(-1px, -1px) rotate(1deg);
  }
  90% {
    transform: translate(1px, 2px) rotate(0deg);
  }
  100% {
    transform: translate(1px, -2px) rotate(-1deg);
  }
}
</style>
