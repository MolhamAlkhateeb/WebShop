<template>
  <div class="hamburger-menu-item">
    <a :href="link"><slot></slot></a>
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";

@Options({
  props: {
    link: String
  }
})
export default class HumburgerMenuItem extends Vue {
  link?: string;
}
</script>

<style scoped lang="scss">
.hamburger-menu-item {
  text-align: center;
  border-bottom: 1px solid rgba(42, 96, 44, 0.5);
  padding: 10px;
  font-size: 1.5em;
  color: var(--green-800);
  &:last-child {
    border-bottom: 0;
    padding-bottom: 20px;
  }
  a,
  a:hover,
  a:focus,
  a:active {
    width: 100%;
    text-decoration: none;
    color: inherit;
    display: block;
    font-weight: 600;
  }
}
</style>
