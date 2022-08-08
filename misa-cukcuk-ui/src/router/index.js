import { createRouter, createWebHistory } from "vue-router";
import Dashboard from "@/views/dashboard/Dashboard.vue";
import MenuList from "@/views/menu/MenuList.vue";

const routes = [
  {
    path: "/",
    name: "dashboard",
    component: <Dashboard />,
  },
  {
    path: "/menu",
    name: "menu",
    component: <MenuList />,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
