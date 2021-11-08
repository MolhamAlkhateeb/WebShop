import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue";
import ProductDetails from "../views/ProductDetails/ProductDetails.vue";
import ContactUs from "../views/ContactUs.vue";
import PrivacyAndPolicy from "@/views/PrivacyPolicy.vue";
import TermsAndConditions from "@/views/TermsAndConditions.vue";
import Register from "@/views/Register.vue"
import Login from "@/components/api-authorization/Login.vue"
import { ApplicationPaths, LoginActions, LogoutActions } from "@/components/api-authorization/ApiAuthorizationConstants";

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
  }, 
  { path:ApplicationPaths.Login , component: Login, props: route => ({action: LoginActions.Login})},
  { path:ApplicationPaths.LoginFailed , component: Login, props: route => ({action: LoginActions.LoginFailed})},
  { path:ApplicationPaths.LoginCallback , component: Login, props: route => ({action: LoginActions.LoginCallback})},
  { path:ApplicationPaths.Profile , component: Login, props: route => ({action: LoginActions.Profile})},
  { path:ApplicationPaths.Register , component: Login, props: route => ({action: LoginActions.Register})},
  { path:ApplicationPaths.LogOut , component: Login, props: route => ({action: LogoutActions.Logout})},
  { path:ApplicationPaths.LogOutCallback , component: Login, props: route => ({action: LogoutActions.LogoutCallback})},
  { path:ApplicationPaths.LoggedOut , component: Login, props: route => ({action: LogoutActions.LoggedOut})},
  

  
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
