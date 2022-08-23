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
  props: ["content", "type", "icon", "disabled"],
  data() {
    return {
      buttonClass: ["button", this.type ? `button-${this.type}` : "button-1"],
    };
  },
  watch: {
    disabled(newVal) {
      if (newVal) {
        this.buttonClass = [
          "button",
          this.type ? `button-${this.type}` : "button-1",
          this.disabled ? "disabled" : null,
        ];
      } else {
        this.buttonClass = [
          "button",
          this.type ? `button-${this.type}` : "button-1",
        ];
      }
    },
  },
  methods: {
    log() {
      console.log("ạkadas");
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/basebutton.css);
</style>