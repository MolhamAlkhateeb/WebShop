<template>
  <div class="footer">
    <div class="footer-items-container">
      <FooterItem title="ORDERS & SUPPORT" :items="ordersAndSupport" />
      <FooterItem title="ABOUT" :items="about" />
      <FooterItem title="FOLLOW US" :items="followUs" />
    </div>

    <FooterBaner />
  </div>
</template>


<script lang="ts">
import { Vue, Options } from "vue-class-component";
import FooterBaner from "@/components/Footer/FooterBaner.vue";
import FooterItem from "@/components/Footer/FooterItem.vue";

@Options({
  components: {
    FooterBaner,
    FooterItem,
  },
})
export default class Footer extends Vue {
  ordersAndSupport = [
    { link: "/", text: "Shipping" },
    { link: "/", text: "Returns" },
    { link: "/", text: "Contact us" },
  ];
  about = [
    { link: "/", text: "Our story" },
    { link: "/", text: "Transparency" },
    { link: "/", text: "Corporates" },
    { link: "/", text: "Become a reseller" },
    { link: "/", text: "Where to find us?" },
  ];
  followUs = [
    // { link: "/", text: "Instagram", icon: "pi-instagram" },
    { link: "/", text: "Facebook", icon: "pi-facebook" },
    { link: "/", text: "Twitter", icon: "pi-twitter" },
  ];
}
</script>

<style scoped>
.footer-items-container {
  display: flex;
  justify-content: space-around;
}
</style>