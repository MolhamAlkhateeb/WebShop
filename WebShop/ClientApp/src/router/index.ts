import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue";
import ProductDetails from "../views/ProductDetails/ProductDetails.vue";
import ContactUs from "../views/ContactUs.vue";
import PrivacyAndPolicy from "@/views/PrivacyPolicy.vue";
import TermsAndConditions from "@/views/TermsAndConditions.vue";
import Register from "@/views/Register.vue"
import Login from "@/views/Login.vue"

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "Home",
    component: Home
  },
  {
    path: "/about",
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue")
  },
  {
    path: "/products/:id",
    name: "ProductDetails",
    component: ProductDetails,
    props: route => ({ id: Number(route.params.id) })
  },
  {
    path: "/login",
    name: "Login",
    component: Login
  },
  {
    path: "/contact-us",
    name: "ContactUs",
    component: ContactUs
  },
  {
    path: "/privacy-and-policy",
    name: "PrivacyAndPolicy",
    component: PrivacyAndPolicy
  },
  {
    path: "/terms-and-conditions",
    name: "TermsAndConditions",
    component: TermsAndConditions
  },
  {
    path: "/register",
    name: "Register",
    component: Register
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
