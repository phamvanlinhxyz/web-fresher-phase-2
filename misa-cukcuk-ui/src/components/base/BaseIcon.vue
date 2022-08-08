<template>
  <div :class="iconClass"></div>
</template>

<script>
export default {
  data() {
    return {
      iconClass: "",
    };
  },
  props: ["iconName"],
  created() {
    this.iconClass = `icon-${this.iconName}`;
  },
};
</script>

<style scoped>
@import url(@/css/base/baseicon.css);
</style>