<template>
  <div v-if="product" class="p-grid">
    <div class="p-grid p-col-12">
      <div class="p-col-12 p-xl-6">
        <Carousel :images="product.images" :count="Number(5)" />
      </div>
      <div class="p-col-12 p-xl-6">
        <div class="row">
          <div class="product-name">{{ product.name }}</div>
        </div>
        <div class="row">
          <small class="product-itemNumber">{{ product.itemNumber }}</small>
        </div>
        <div class="row">
          <div class="product-description">{{ product.description }}</div>
        </div>
        <div>
          <div v-for="(price, index) of product.prices" :key="index">
            <div>{{ price.minQuantity }} - {{ price.maxQuantity }}</div>
            <h4>{{ price.price }} {{ price.currency }}</h4>
          </div>
        </div>
        <div class="row">
          <div class="btn" @click="getQuoteClick">GET A QUOTE</div>
          <div class="btn">ORDER A SAMPLE</div>
        </div>
      </div>
    </div>
    <div class="p-col-12">
      <TabView class="p-col-12 p-xl-6">
        <TabPanel header="Specifications">
          <table class="specs-table p-col-12">
            <tbody>
              <tr v-for="(spec, index) of product.specifications" :key="index">
                <td>{{ spec.header }}</td>
                <td>{{ spec.text }}</td>
              </tr>
            </tbody>
          </table>
        </TabPanel>
      </TabView>
    </div>
    <QuoteDialog
      v-model:show="showGetQuoteDialog"
      :itemNumber="product.itemNumber"
      :productId="product.id"
    />
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
import TabView from "primevue/tabview";
import TabPanel from "primevue/tabpanel";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";
import InputNumber from "primevue/inputnumber";
import Textarea from "primevue/textarea";
import QuoteDialog from "@/views/ProductDetails/QuoteDialog.vue";

@Options({
  props: {
    id: Number,
  },
  components: {
    Galleria,
    Button,
    Carousel,
    TabView,
    TabPanel,
    Dialog,
    InputText,
    InputNumber,
    Textarea,
    QuoteDialog,
  },
})
export default class ProductDetails extends Vue {
  product: Product | null = null;
  showGetQuoteDialog = false;
  id = 0;
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

  getQuoteClick() {
    this.showGetQuoteDialog = true;
  }
}
</script>

<style scoped>
.container {
  display: flex;
  max-width: 100%;
  width: 100%;
  margin-bottom: 1em;
}

.container > * {
  width: 50%;
}

.product-showcase {
  width: 50%;
}

.product-info {
  text-align: left;
  width: 50%;
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

.product-itemNumber {
  background-color: var(--var-main-color);
  color: var(--var-secondary-color);
  padding: 0.25em;
}

.p-datatable-thead {
  display: none !important;
}
.specs-table {
  border-spacing: 0;
  text-align: start;
}
.specs-table tr td {
  padding: 0.5rem 1rem;
}
.specs-table tr:nth-child(odd) {
  background-color: #f1ece6;
}
.specs-table tr td:nth-child(1) {
  font-weight: 700;
}

@media only screen and (max-width: 990px) {
  .container {
    flex-direction: column;
  }
  .product-showcase {
    width: 100%;
  }

  .product-info {
    width: 100%;
  }
}
</style>