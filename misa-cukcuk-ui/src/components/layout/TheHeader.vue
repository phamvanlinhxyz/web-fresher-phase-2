<template>
  <div class="header">
    <div class="header-left">
      <base-icon
        iconName="left-header"
        class="header-left-icon"
        @click="toggleMenu"
      />
      <div class="header-left-logo"></div>
    </div>
    <div class="header-right">
      <div>
        <div class="header-right-menu menu-1">
          <div class="menu-input">misacukcuk</div>
          <div class="menu-icon">
            <base-icon iconName="white-drop-arrow" />
          </div>
        </div>
      </div>
      <div style="display: flex; align-items: center">
        <div class="header-right-menu menu-2">
          <div class="menu-input">Tiếng Việt (Tiếng Việt)</div>
          <div class="menu-icon">
            <base-icon iconName="white-drop-arrow" />
          </div>
        </div>
        <base-icon class="header-icon" iconName="notification" />
        <div class="span3">
          <i class="globe-icon icon-globe">&#xe800;</i>
        </div>
        <div class="header-user">
          <div class="user-avatar"></div>
          <div class="user-name">Linh</div>
        </div>
        <base-icon iconName="white-drop-arrow" />
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  methods: {
    ...mapActions(["toggleMenu"]),
  },
};
</script>

<style scoped>
@import url(@/css/layouts/theheader.css);
</style>