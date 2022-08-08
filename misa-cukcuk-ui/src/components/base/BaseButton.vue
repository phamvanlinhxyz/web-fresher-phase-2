<template>
  <button :class="buttonClass">
    <!-- Icon -->
    <div class="button-icon" v-if="icon">
      <base-icon :iconName="icon" />
    </div>
    <!-- Nội dung của button -->
    <div class="button-text">
      {{ content }}
    </div>
  </button>
</template>

<script>
export default {
  props: ["content", "type", "icon"],
  data() {
    return {
      buttonClass: ["button", this.type ? `button-${this.type}` : "button-1"],
    };
  },
};
</script>

<style scoped>
@import url(@/css/base/basebutton.css);
</style>