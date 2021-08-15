<template>
  <div class="accordion-tab">
    <div class="accordion-tab__header" @click="onClickHandler()">
      <svg
        :class="svgClassName"
        class="accordion-tab__icon"
        viewBox="0 0 24 24"
        width="25px"
        height="25px"
      >
        <path d="M6 9L12 15L18 9" stroke="currentColor" fill="none" />
      </svg>
      <span>{{ header }}</span>
    </div>

    <div class="accordion-tab__content" :class="contentClass">
      <slot></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { Vue, Options } from "vue-class-component";

@Options({
  props: {
    header: String
  }
})
export default class AccordionTab extends Vue {
  header?: string;
  contentClass = "";
  svgClassName = "";
  isExpanded = false;
  onClickHandler() {
    this.isExpanded = !this.isExpanded;
    this.svgClassName = this.isExpanded ? "expanded" : "";
    this.contentClass = this.isExpanded ? "expanded" : "";
  }
}
</script>

<style scoped lang="scss">
.accordion-tab {
  // border-bottom: 1px solid black;
  border-bottom: 1px solid #dee2e6;
  border-left: 1px solid #dee2e6;
  border-right: 1px solid #dee2e6;
  &:first-child {
    border-top: 1px solid #dee2e6;
  }

  &__content {
    overflow: hidden;
    max-height: 0;
    transition: max-height 0.5s;
    -webkit-transition: max-height 0.5s;
    padding-left: 4em;
    padding-right: 4em;
    &.expanded {
      max-height: 1000px;
      overflow: auto;
    }
  }
  &__icon {
    transform: rotate(-90deg);
    transition: transform 160ms ease-in-out;
    stroke-width: 2px;
    &.expanded {
      transform: rotate(0deg);
    }
  }
  &__header {
    display: flex;
    flex-direction: row;
    padding: 1rem;
    background: #f8f9fa;
    border-bottom: 1px solid #dee2e6;
    & span {
      font-weight: 600;
      text-align: left;
      padding-left: 0.5em;
      width: 100%;
    }
  }
}
</style>
