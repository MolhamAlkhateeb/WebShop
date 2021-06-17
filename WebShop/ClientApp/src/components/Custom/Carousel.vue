<template>
  <div class="container">
    <div class="main-image-container">
      <div class="nav-btn prev">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          viewBox="0 0 24 24"
          style="transform: scale(-1, 1)"
        >
          <path
            d="M7.33 24l-2.83-2.829 9.339-9.175-9.339-9.167 2.83-2.829 12.17 11.996z"
          />
        </svg>
      </div>

      <div class="main-image">
        <img :src="mainImage" />
      </div>
      <div class="nav-btn next">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          viewBox="0 0 24 24"
        >
          <path
            d="M7.33 24l-2.83-2.829 9.339-9.175-9.339-9.167 2.83-2.829 12.17 11.996z"
          />
        </svg>
      </div>
    </div>
    <div class="thumbnails-container">
      <!-- <img
        v-for="(image, index) of images.slice(0, count)"
        :key="index"
        :src="image"
      /> -->
    </div>
  </div>
</template>
<script lang="ts">
import { Vue, Options } from "vue-class-component";

@Options({
  props: {
    images: Array as () => Array<string>,
    count: {
      type: Number,
      default: (props: any) => props.images.length,
    },
  },
})
export default class Carousel extends Vue {
  mainImage = "";
  images = new Array<string>();
  count = 0;
  mounted() {
    this.mainImage = this.images[0];
  }
}
</script>
<style scoped>
.container {
  display: flex;
  flex-direction: column;
  height: 100%;
}
.main-image-container {
  width: 100%;
  padding-top: 100%; /* 1:1 Aspect Ratio */
  position: relative;
}
.main-image {
  width: 100%;
  position: absolute;
  top: 0;
}
.main-image img {
  width: 100%;
}
.thumbnails-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;
  height: 20%;
  padding: 10px;
  background: rgba(0, 0, 0, 0.9);
}
.thumbnails-container img {
  height: 100%;
}
.next {
  margin-left: auto;
}

.nav-btn {
  z-index: 2;
  display: flex;
  align-items: center;
  position: absolute;
  top: calc(50% - 12px);
}
.nav-btn.prev {
  left: 0;
}
.nav-btn.next {
  right: 0;
}
</style>