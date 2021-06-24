<template>
  <div>
    <div class="banner">
    </div>
    <ProductList />
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import ProductList from "@/components/ProductList.vue";
@Options({
  components: {
    ProductList,
  },
})
export default class Home extends Vue {}
</script>
<style scoped>

.banner{
  padding-bottom: 25%
  ;
 background-image: url("/images/banner.jpg"); 
 background-position: center;
}
</style>
