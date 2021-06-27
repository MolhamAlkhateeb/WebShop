<template>
  <div v-if="product">
    <div class="container">
      <div class="product-showcase">
        <Carousel
          :images="product.images"
          :count="Number(5)"
        />
      </div>
      <div class="product-info">
        <div class="row">
          <div class="product-name">{{ product.name }}</div>
        </div>
        <div class="row">
          <div class="product-description">{{ product.description }}</div>
        </div>

        <div class="row">
          <Button>Add To Cart</Button>
          <Button>Order Sample</Button>
        </div>
      </div>
    </div>
  </div>
  <div v-else>...Loading</div>
</template>


<script lang="ts">
import axios from "axios";
import { Options, Vue } from "vue-class-component";
import Galleria from "primevue/galleria";
import Button from "primevue/button";
import Carousel from "@/components/Custom/Carousel.vue";
import Product from "@/models/Product";

@Options({
  props: {
    id: Number,
  },
  components: {
    Galleria,
    Button,
    Carousel,
  },
})
export default class ProductDetails extends Vue {
  product: Product | null = null;
  id = 0;
  responsiveOptions = [
    {
      breakpoint: "1024px",
      numVisible: 5,
    },
    {
      breakpoint: "768px",
      numVisible: 3,
    },
    {
      breakpoint: "560px",
      numVisible: 1,
    },
  ];
  responsiveOptions2 = [
    {
      breakpoint: "768px",
      numVisible: 3,
    },
    {
      breakpoint: "560px",
      numVisible: 1,
    },
  ];
  async mounted() {
    this.product = await this.getProduct();
    if (this.product)
      this.product.images = this.product?.images?.sort(
        (a, b) => (a.position || 0) - (b.position || 0)
      );
  }

  async getProduct() {
    const response = await axios.get(`/api/products/${this.id}`);
    return response.data;
  }
}
</script>

<style scoped>
.container {
  display: flex;
  max-width: 100%;
}

.product-showcase {
  flex: 0 0 60%;
  max-width: 100%;
}

.product-info {
  flex: 0 0 40%;
  text-align: left;
}

.product-name {
  font-size: 1.5rem;
  font-weight: 700;
}

.row {
  display: flex;
  margin: 0 0 1rem 0;
}
.row *:not(:first-child) {
  margin-left: 1.5rem;
}

@media only screen and (max-width: 990px) {
  .container {
    flex-direction: column;
  }
}
</style>