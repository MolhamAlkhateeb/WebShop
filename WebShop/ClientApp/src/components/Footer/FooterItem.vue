<template>
  <ul>
    <span class="title">{{ title }}</span>
    <li v-for="(item, index) of items" :key="index">
      <i v-if="item.icon" class="pi" :class="item.icon"></i>
      <router-link :to="item.link">{{ item.text }}</router-link>
    </li>
  </ul>
</template>


<script lang="ts">
import { Vue, Options } from "vue-class-component";
@Options({
  props: {
    title: String,
    items: Array,
  },
})
export default class FooterItem extends Vue {
  title = "";
  items = new Array<any>();
}
</script>
<style scoped>
ul {
  list-style: none;
  text-align: left;
}
.title {
  color: #909090;
}

li {
  padding-top: 2.5px;
  padding-bottom: 2.5px;
}

i {
  margin-right: 0.5em;
}

a {
  color: inherit;
  background-color: transparent;
  text-decoration: none;
}
</style>