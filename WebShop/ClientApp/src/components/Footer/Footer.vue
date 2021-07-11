<template>
  <div class="p-grid footer">
    <div class="p-grid p-col-12 p-justify-around footer-grid">
      <div class="p-col-12 p-xl-2">
        <FooterItem title="ORDERS & SUPPORT" :items="ordersAndSupport" />
      </div>
      <div class="p-col-12 p-xl-2">
        <FooterItem title="ABOUT" :items="about" />
      </div>
      <div class="p-col-12 p-xl-2">
        <FooterItem title="FOLLOW US" :items="followUs" />
      </div>
    </div>

    <div class="footer-accordion p-col-12">
      <Accordion :multiple="true">
        <AccordionTab header="ORDERS & SUPPORT">
          <FooterItem title="" :items="ordersAndSupport" />
        </AccordionTab>
        <AccordionTab>
          <template #header>
            <span>ABOUT</span>
          </template>
          <FooterItem title="" :items="about" />
        </AccordionTab>
        <AccordionTab header="FOLLOW US">
          <FooterItem title="" :items="followUs" />
        </AccordionTab>
      </Accordion>
    </div>

    <div class="p-col-12">
      <FooterBaner />
    </div>
  </div>
</template>


<script lang="ts">
import { Vue, Options } from "vue-class-component";
import FooterBaner from "@/components/Footer/FooterBaner.vue";
import FooterItem from "@/components/Footer/FooterItem.vue";
import Accordion from "primevue/accordion";
import AccordionTab from "primevue/accordiontab";

@Options({
  components: {
    FooterBaner,
    FooterItem,
    Accordion,
    AccordionTab,
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

<style lang="scss" scoped>
.footer {
  background-color: var(--var-main-color);
  color: var(--var-secondary-color);
}

.footer-grid {
  display: none;
}

::v-deep(.footer-accordion) {
  .p-accordion-header-link {
    background-color: var(--var-main-color);
  }
}

@media (min-width: 1025px) {
  .footer-accordion {
    display: none;
  }

  .footer-grid {
    display: flex;
  }
}
</style>