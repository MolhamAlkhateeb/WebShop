import { createRouter, createWebHistory } from "vue-router";
import Home from "../views/Home.vue";
import ProductDetails from "../views/ProductDetails.vue";
const routes = [
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
        component: () => import(/* webpackChunkName: "about" */ "../views/About.vue")
    },
    {
        path: "/products/:id",
        name: "ProductDetails",
        component: ProductDetails,
        props: route => ({ id: Number(route.params.id) })
    }
];
const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});
export default router;
//# sourceMappingURL=index.js.map