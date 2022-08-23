<template>
  <div class="container">
    <the-header />
    <div class="main-body">
      <the-menu v-if="isShowMenu" />
      <the-content />
    </div>
  </div>
</template>

<script>
import TheHeader from "@/components/layout/TheHeader.vue";
import TheMenu from "@/components/layout/TheMenu.vue";
import TheContent from "./components/layout/TheContent.vue";
import { mapState } from "vuex";

export default {
  components: { TheHeader, TheMenu, TheContent },
  computed: mapState({
    isShowMenu: (state) => state.app.isShowMenu,
  }),
};
</script>

<style>
@import url(@/css/main.css);
</style>
