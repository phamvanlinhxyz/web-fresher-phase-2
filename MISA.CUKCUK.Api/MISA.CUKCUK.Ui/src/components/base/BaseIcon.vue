<template>
  <div :class="iconClass"></div>
</template>

<script>
export default {
  data() {
    return {
      iconClass: [],
    };
  },
  props: ["iconName", "disabled"],
  watch: {
    disabled(newVal) {
      console.log(newVal);
      if (newVal) {
        this.iconClass = [`icon-${this.iconName}`, "disabled"];
      } else {
        this.iconClass = [`icon-${this.iconName}`];
      }
    },
  },
  created() {
    if (this.disabled) {
      this.iconClass = [`icon-${this.iconName}`, "disabled"];
    } else {
      this.iconClass = [`icon-${this.iconName}`];
    }
  },
};
</script>

<style scoped>
@import url(@/css/base/baseicon.css);
</style>