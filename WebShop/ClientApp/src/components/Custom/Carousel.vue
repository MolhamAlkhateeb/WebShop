<template>
  <div class="container">
    <div class="main-image" v-touch:swipe="swipeHandler">
      <img :src="mainImage.url" />
    </div>
    <div class="thumbnails">
      <div
        v-for="(image, index) of images"
        :key="index"
        class="thumbnail"
        @click="selectImage(image)"
      >
        <img :src="image.url" />
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import Image from "@/models/Image";
import { Vue, Options } from "vue-class-component";

@Options({
  props: {
    images: Array as () => Array<Image>,
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

  selectImage(image: string) {
    this.mainImage = image;
  }

  swipeHandler(direction: any, event: any) {
    if (direction == "right") {
      this.selectNext();
    }
    if (direction == "left") {
      this.selectPrev();
    }
  }

  selectPrev() {
    const currentIndex = this.images.indexOf(this.mainImage);
    const index =
      currentIndex - 1 <= 0 ? this.images.length - 1 : currentIndex - 1;
    this.mainImage = this.images[index];
  }

  selectNext() {
    const currentIndex = this.images.indexOf(this.mainImage);
    const index = currentIndex + 1 >= this.images.length ? 0 : currentIndex + 1;
    this.mainImage = this.images[index];
  }
}
</script>
<style scoped>
.container {
  display: flex;
  justify-content: center;
  flex-direction: column;
  height: 50vh;
}
.main-image {
  width: 100%;
  height: 85%;
}

.main-image img {
  width: 100%;
  height: 100%;
  object-fit: contain;
  overflow: hidden;
}

.thumbnails {
  width: 100%;
  height: 15%;
  background-color: rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: row;
  padding: 0.5em;
  overflow-x: auto;
  justify-content: space-between;
}

.thumbnail {
  /* border: 1px solid grey; */
  height: 100%;
  display: inline-block;
  cursor: pointer;
  padding: 0.125em;
}

.thumbnail img:hover {
  opacity: 0.5;
}

.thumbnail:not(:last-child) {
  margin-right: 1em;
}

.thumbnail img {
  height: 100%;
}
</style>
