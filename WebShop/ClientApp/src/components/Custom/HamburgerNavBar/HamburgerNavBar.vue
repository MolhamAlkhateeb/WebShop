<template>
  <div class="hamburger-container">
    <div class="hamburger-header">
      <img src="@/assets/onlyLogoWhite.png" class="logo" />
      <img src="@/assets/onlyTextWhite.png" class="text" />
      <div class="hamburger-icon">
        <HamburgerIcon @click="onClickHandler()" />
      </div>
    </div>
    <div class="hamburger-content" :class="contentClass">
      <slot></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import HamburgerIcon from "@/components/SVG/HamburgerIcon.vue";

@Options({
  components: { HamburgerIcon }
})
export default class Hamburger extends Vue {
  contentClass = "";
  isExpanded = false;
  onClickHandler() {
    this.isExpanded = !this.isExpanded;
    this.contentClass = this.isExpanded ? "expanded" : "";
  }
}
</script>

<style scoped lang="scss">
.hamburger-container {
  background-color: var(--var-main-color);
  position: -webkit-sticky; /* Safari */
  position: sticky; // sticky cannot work with a parent with overflow hidden
  top: 0;
  z-index: 5; // with out this some element will render over
}
.hamburger-content {
  width: 100%;
  background-color: var(--var-secondary-color);
  border-bottom: 2px solid rgba(73, 80, 87, 0.5);
  overflow: hidden;
  max-height: 0;
  transition: max-height 0.5s;
  -webkit-transition: max-height 0.5s;
  &.expanded {
    max-height: 500px;
    overflow: auto;
  }
}

.hamburger-header {
  height: 80px;
  color: var(--var-secondary-color);
  border: 1px solid black;
  padding: 16px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  .logo {
    height: 100%;
  }
  .text {
    height: 70%;
  }
  .hamburger-icon {
    color: var(--var-secondary-color);
    float: right;
    svg {
      height: 40px;
    }
  }
}
</style>
