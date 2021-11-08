<template>
  <div class="price-bar-container p-grid p-nogutter">
    <transition name="fade">
      <div v-if="quoteExpanded" class="price-bar-modal"></div>
    </transition>
    <transition name="slide-y">
      <PriceBarPanel v-if="quoteExpanded" :product="product" />
    </transition>
    <div class="price-bar p-grid p-nogutter p-justify-between p-p-2 p-col-12">
      <div class="p-xl-4 p-col-12 p-pb-2">
        <span class="description">{{ product.name }}</span>
      </div>
      <div class="p-grid p-col-12 p-nogutter p-justify-between p-xl-4">
        <div class="p-xl-6 p-col-6 p-pr-1">
          <div
            class="btn-reverse p-grid p-nogutter p-justify-between"
            @click="getQuoteClick()"
          >
            <span>GET A QUOTE</span>
            <i
              :class="`arrow-icon pi pi-angle-up p-grid p-dir-col p-nogutter p-justify-center ${
                quoteExpanded ? 'flip' : ''
              }`"
            ></i>
          </div>
        </div>
        <div class="p-xl-6 p-col-6 p-pl-1">
          <div class="btn-reverse" @click="getQuoteClick()">ORDER A SAMPLE</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import PriceBarPanel from "@/components/PriceBar/PriceBarPanel.vue";

@Options({
  props: {
    product: {
      type: Object,
      required: true,
    },
  },
  components: {
    PriceBarPanel,
  },
})
export default class PriceBar extends Vue {
  quoteExpanded = false;
  getQuoteClick(): void {
    this.quoteExpanded = !this.quoteExpanded;
  }
}
</script>

<style lang="scss" scoped>
.price-bar-container {
  width: 90vw;
  position: fixed;
  left: 5vw;
  bottom: 8px;
  z-index: 100;
}
.price-bar {
  background-color: var(--var-main-color);
  border: 1px solid black;
  z-index: 101;
}

.description {
  font-weight: 700;
  color: var(--var-secondary-color);
  font-size: 1.25rem;
}

.arrow-icon {
  transition: transform 0.25s linear;
}

.flip {
  transform: rotate(180deg);
}

.price-bar-modal {
  position: fixed;
  top: 0;
  left: 0;
  background-color: rgba(0, 0, 0, 0.5);
  width: 100vw;
  height: 100vh;
  z-index: 99;
}

@media only screen and (max-width: 990px) {
  .price-bar-container {
    width: 100vw;
    left: 0;
    flex-direction: column;
  }
}
</style>