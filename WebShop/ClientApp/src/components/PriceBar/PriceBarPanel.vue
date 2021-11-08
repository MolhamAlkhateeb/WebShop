<template>
  <div class="price-bar-panel p-grid p-nogutter p-col-12 p-p-4">
    <GetQuoteForm />
  </div>
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import GetQuoteForm from "@/components/forms/GetQuoteForm.vue";

@Options({
  props: {
    product: {
      type: Object,
      required: true,
    },
  },
  components: {
    GetQuoteForm,
  },
})
export default class PriceBarPanel extends Vue {}
</script>

<style lang="scss" scoped>
.price-bar-panel {
  background-color: white;
  z-index: 101;
}
</style>