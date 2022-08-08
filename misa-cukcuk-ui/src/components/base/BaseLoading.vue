<template>
  <div class="loading-wrapper">
    <div class="loading">
      <img
        src="http://cukcukcomnew.misacdn.net/QLNH/resources/images/loadmask/loading.gif"
        width="16"
        height="16"
      />
      <div class="loading-text">{{ content }}...</div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["content"],
};
</script>

<style scoped>
@import url(@/css/base/baseloading.css);
</style>