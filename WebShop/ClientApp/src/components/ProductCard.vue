<template>
  <div id="product-card">
    <router-link :to="{ name: 'ProductDetails', params: { id: product.id } }">
      <div id="img-container">
        <img v-for="(image, index) in product.images" :key="index" :src="image.url" />
      </div>
      <h4>{{ product.name }}</h4>
    </router-link>
    <p>{{ product.description }}</p>
    <button>Add To Cart</button>
  </div>
</template>


<script>
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    product: Object,
  },
})
export default class ProductCard extends Vue {}
</script>


<style scoped>
#product-card {
  max-width: 20%;
}

#img-container img {
  max-width: 100%;
}
</style>