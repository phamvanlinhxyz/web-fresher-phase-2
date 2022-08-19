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
import resources from "@/resources";
import { mapState } from "vuex";
import BaseIcon from "../base/BaseIcon.vue";
export default {
  components: { BaseIcon },
  computed: mapState({
    langCode: (state) => state.app.langCode,
  }),
  data() {
    return {
      menuItems: [],
    };
  },
  created() {
    this.menuItems = [
      {
        path: "/",
        name: resources[`${this.langCode}_Menu_Item`].dashboard,
        icon: "dashboard",
      },
      {
        path: "/menu",
        name: resources[`${this.langCode}_Menu_Item`].menu,
        icon: "menu",
      },
    ];
  },
};
</script>

<style scoped>
@import url(@/css/layouts/themenu.css);
</style>