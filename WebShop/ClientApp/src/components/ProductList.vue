<template>
  <DataView
    :value="products"
    :layout="layout"
    :paginator="true"
    :rows="9"
    :sortOrder="sortOrder"
    :sortField="sortField"
  >
    <template #header>
      <div class="p-grid p-nogutter">
        <div class="p-col-6" style="text-align: left">
          <Dropdown
            v-model="sortKey"
            :options="sortOptions"
            optionLabel="label"
            placeholder="Sort By Price"
            @change="onSortChange($event)"
          />
        </div>
        <div class="p-col-6" style="text-align: right">
          <DataViewLayoutOptions v-model="layout" />
        </div>
      </div>
    </template>

    <template #list="slotProps">
      <div class="p-col-12">
        <div class="product-list-item">
          <img
            :src="
              slotProps.data.images?.find((i) => i.isMain)?.url ||
              slotProps.data.images[0]?.url
            "
            :alt="slotProps.data.name"
          />
          <div class="product-list-detail">
            <div class="product-name">{{ slotProps.data.name }}</div>
            <div class="product-description">
              {{ slotProps.data.description }}
            </div>
            <i class="pi pi-tag product-category-icon"></i
            ><span class="product-category">{{ slotProps.data.category }}</span>
          </div>
          <div class="product-list-action">
            <span class="product-price">${{ slotProps.data.price }}</span>
            <Button
              icon="pi pi-shopping-cart"
              label="Add to Cart"
              :disabled="slotProps.data.inventoryStatus === 'OUTOFSTOCK'"
            ></Button>
          </div>
        </div>
      </div>
    </template>

    <template #grid="slotProps">
      <div class="p-col-12 p-md-4">
        <div class="product-grid-item card">
          <div class="product-grid-item-top">
            <div>
              <i class="pi pi-tag product-category-icon"></i>
              <span class="product-category">{{
                slotProps.data.category
              }}</span>
            </div>
          </div>
          <div
            class="product-grid-item-content"
            @click="openProductDetails(slotProps.data.id)"
          >
            <img
              :src="
                slotProps.data.images?.find((i) => i.isMain)?.url ||
                slotProps.data.images[0]?.url
              "
              :alt="slotProps.data.name"
            />
            <div class="product-name">{{ slotProps.data.name }}</div>
            <div class="product-description">
              {{ slotProps.data.description }}
            </div>
          </div>
          <div class="product-grid-item-bottom">
            <span class="product-price">${{ slotProps.data.price }}</span>
            <Button
              icon="pi pi-shopping-cart"
              :disabled="slotProps.data.inventoryStatus === 'OUTOFSTOCK'"
            ></Button>
          </div>
        </div>
      </div>
    </template>
  </DataView>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import ProductCard from "@/components/ProductCard.vue";
import ProductService from "../services/ProductService";
import Product from "../models/Product";
import DataView from "primevue/dataview";
import Dropdown from "primevue/dropdown";
import Button from "primevue/button";
import DataViewLayoutOptions from "primevue/dataviewlayoutoptions";
import Rating from "primevue/rating";
import Galleria from "primevue/galleria";
import router from "@/router";

@Options({
  components: {
    ProductCard,
    DataView,
    Dropdown,
    Button,
    DataViewLayoutOptions,
    Rating,
    Galleria,
  },
})
export default class ProductList extends Vue {
  productService?: ProductService;
  products?: Product[] = new Array<Product>();
  layout = "grid";
  sortKey = null;
  sortOrder: number | null = null;
  sortField = null;
  sortOptions: any[] = [
    { label: "Price High to Low", value: "!price" },
    { label: "Price Low to High", value: "price" },
  ];
  created() {
    this.productService = new ProductService();
  }
  async mounted() {
    this.products = await this.productService?.getProducts();
  }

  onSortChange(event: any) {
    const value = event.value.value;
    const sortValue = event.value;

    if (value.indexOf("!") === 0) {
      this.sortOrder = -1;
      this.sortField = value.substring(1, value.length);
      this.sortKey = sortValue;
    } else {
      this.sortOrder = 1;
      this.sortField = value;
      this.sortKey = sortValue;
    }
  }

  openProductDetails(id: number) {
    router.push({ name: "ProductDetails", params: { id: id } });
  }
}
</script>

<style lang="scss" scoped>
.p-dropdown {
  width: 14rem;
  font-weight: normal;
}

.product-name {
  font-size: 1.5rem;
  font-weight: 700;
}

.product-description {
  margin: 0 0 1rem 0;
}

.product-category-icon {
  vertical-align: middle;
  margin-right: 0.5rem;
}

.product-category {
  font-weight: 600;
  vertical-align: middle;
}

::v-deep(.product-list-item) {
  display: flex;
  align-items: center;
  padding: 1rem;
  width: 100%;

  img {
    width: 50px;
    box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23);
    margin-right: 2rem;
  }

  .product-list-detail {
    flex: 1 1 0;
  }

  .p-rating {
    margin: 0 0 0.5rem 0;
  }

  .product-price {
    font-size: 1.5rem;
    font-weight: 600;
    margin-bottom: 0.5rem;
    align-self: flex-end;
  }

  .product-list-action {
    display: flex;
    flex-direction: column;
  }

  .p-button {
    margin-bottom: 0.5rem;
  }
}

::v-deep(.product-grid-item) {
  margin: 0.5rem;
  border: 1px solid #dee2e6;

  background: var(--surface-e);
  padding: 2rem;
  -webkit-box-shadow: 0 2px 1px -1px rgb(0 0 0 / 20%),
    0 1px 1px 0 rgb(0 0 0 / 14%), 0 1px 3px 0 rgb(0 0 0 / 12%);
  box-shadow: 0 2px 1px -1px rgb(0 0 0 / 20%), 0 1px 1px 0 rgb(0 0 0 / 14%),
    0 1px 3px 0 rgb(0 0 0 / 12%);
  border-radius: 4px;
  margin-bottom: 2rem;

  .product-grid-item-top,
  .product-grid-item-bottom {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }

  img {
    box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16), 0 3px 6px rgba(0, 0, 0, 0.23);
    margin: 2rem 0;
    width: 75%;
    min-width: 75%;
  }

  .product-grid-item-content {
    text-align: center;
    cursor: pointer;
  }

  .product-price {
    font-size: 1.5rem;
    font-weight: 600;
  }
}

@media screen and (max-width: 576px) {
  .product-list-item {
    flex-direction: column;
    align-items: center;

    img {
      margin: 2rem 0;
    }

    .product-list-detail {
      text-align: center;
    }

    .product-price {
      align-self: center;
    }

    .product-list-action {
      display: flex;
      flex-direction: column;
    }

    .product-list-action {
      margin-top: 2rem;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      width: 100%;
    }
  }
}
</style>