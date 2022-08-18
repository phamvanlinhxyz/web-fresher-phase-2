<template>
  <div class="menu">
    <ul class="menu-items">
      <li class="menu-item" v-for="(item, index) of menuItems" :key="index">
        <router-link :to="item.path" class="menu-item-router">
          <base-icon :iconName="item.icon" />
          <div class="menu-item-name">{{ item.name }}</div>
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script>
import BaseIcon from "../base/BaseIcon.vue";
export default {
  components: { BaseIcon },
  data() {
    return {
      menuItems: [
        {
          path: "/",
          name: "Tổng quan",
          icon: "dashboard",
        },
        {
          path: "/menu",
          name: "Thực đơn",
          icon: "menu",
        },
      ],
    };
  },
};
</script>

<style scoped>
@import url(@/css/layouts/themenu.css);
</style>