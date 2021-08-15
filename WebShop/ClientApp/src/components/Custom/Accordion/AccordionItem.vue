<template>
  <div class="accordion-item">
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
export default class AccordionItem extends Vue {
  link?: string;
}
</script>

<style scoped lang="scss">
.accordion-item {
  text-align: left;
  border-bottom: 1px solid rgba(42, 96, 44, 0.5);
  padding: 10px;
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
