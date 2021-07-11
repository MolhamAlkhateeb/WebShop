<template>
  <router-link :to="to" class="menu-item">
    {{ text }}
  </router-link>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    to: String,
    text: String,
  },
})
export default class MenuItem extends Vue {
  to = "";
  text = "";
}
</script>

<style scoped>
.menu-item {
  color: var(--var-secondary-color);
  display: inline-block;
  cursor: pointer;
  text-decoration: none;
  padding: 0.25em 0.5em;
  border-radius: 0.25em;
  position: relative;
}

.menu-item:not(:last-child) {
  margin-right: 1em;
}

.menu-item::before {
  content: "";
  position: absolute;
  top: 110%;
  left: 0;
  height: 0.125em;
  width: 100%;
  transform: scaleX(0);
  background: var(--var-secondary-color);
  pointer-events: none;
  transition: transform 0.3s ease-in-out;
  background-color: var(--var-secondary-color);
}

.menu-item:hover:before {
  transform: scaleX(1);
}


.router-link-active:before {
  transform: scaleX(1);
}
</style>
