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
      <Accordion>
        <AccordionTab header="ORDERS &amp; SUPPORT">
          <AccordionItem link="TODO">Shipping</AccordionItem>
          <AccordionItem link="TODO">Return</AccordionItem>
          <AccordionItem link="TODO">Contact us</AccordionItem>
        </AccordionTab>
        <AccordionTab header="ABOUT">
          <AccordionItem link="TODO">Our story</AccordionItem>
          <AccordionItem link="TODO">Transparency</AccordionItem>
          <AccordionItem link="TODO">Corporates</AccordionItem>
          <AccordionItem link="TODO">Become a reseller</AccordionItem>
          <AccordionItem link="TODO">Whare to find us ?</AccordionItem>
        </AccordionTab>
        <AccordionTab header="FOLLOW US">
          <AccordionItem link="TODO">
            <FacebookIcon class="some-icon" />
            Facebook
          </AccordionItem>
          <AccordionItem link="TODO">
            <TwitterIcon class="some-icon" />
            Twitter
          </AccordionItem>
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
import Accordion from "@/components/Custom/Accordion/Accordion.vue";
import AccordionTab from "@/components/Custom/Accordion/AccordionTab.vue";
import AccordionItem from "@/components/Custom/Accordion/AccordionItem.vue";
import FacebookIcon from "@/components/SVG/FacebookLogo.vue";
import TwitterIcon from "@/components/SVG/TwitterLogo.vue";

@Options({
  components: {
    FooterBaner,
    FooterItem,
    Accordion,
    AccordionTab,
    AccordionItem,
    FacebookIcon,
    TwitterIcon
  }
})
export default class Footer extends Vue {
  ordersAndSupport = [
    { link: "/", text: "Shipping" },
    { link: "/", text: "Returns" },
    { link: "/", text: "Contact us" }
  ];
  about = [
    { link: "/", text: "Our story" },
    { link: "/", text: "Transparency" },
    { link: "/", text: "Corporates" },
    { link: "/", text: "Become a reseller" },
    { link: "/", text: "Where to find us?" }
  ];
  followUs = [
    // { link: "/", text: "Instagram", icon: "pi-instagram" },
    { link: "/", text: "Facebook", icon: "pi-facebook" },
    { link: "/", text: "Twitter", icon: "pi-twitter" }
  ];
}
</script>

<style lang="scss" scoped>
.some-icon {
  width: 20px;
  height: 20px;
  vertical-align: -3px;
}
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
