<template>
  <div v-if="product">
    <img :src="product.images[0].url" />
    <h1>{{ product.id }}</h1>
  </div>
  <div v-else>...Loading</div>
</template>


<script lang="ts">
import axios from "axios";
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    id: Number,
  },
})
export default class ProductDetails extends Vue {
  product?: any = null;
  async mounted() {
    this.product = await this.getProduct();
  }

  async getProduct() {
    const id = Object.getOwnPropertyDescriptor(this.$props, "id")?.value;
    const response = await axios.get(`/api/products/${id}`);
    return response.data;
  }
}
</script>

<style scoped>
</style>