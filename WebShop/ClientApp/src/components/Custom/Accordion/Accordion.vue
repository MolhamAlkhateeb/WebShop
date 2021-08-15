<template>
  <div class="accordion-container"><slot></slot></div>
</template>

<script lang="ts">
import { Vue } from "vue-class-component";
export default class Accordion extends Vue {}
</script>

<style scoped>
.accordion-container {
  color: var(--green-800);
  background-color: var(--surface-a);
}
</style>
