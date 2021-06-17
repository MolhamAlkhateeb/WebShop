<template>
  <div v-if="product">
    <div class="container">
      <div class="product-showcase">
        <Carousel
          :images="product.images.map((i) => i.url)"
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
  product?: any = null;
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
    const g = new Array<any>();
    g.push(this.product.images[0]);
    g.push(this.product.images[0]);
    g.push(this.product.images[0]);
    g.push(this.product.images[0]);
    g.push(this.product.images[0]);
    g.push(this.product.images[0]);
    this.product.images = g;
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
  margin: 0 1.5rem;
}

.product-showcase {
  flex: 0 0 60%;
}

.product-info {
  flex: 0 0 40%;
  text-align: left;
}

.p-galleria-thumbnail-items-container {
  height: 100%;
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
</style>