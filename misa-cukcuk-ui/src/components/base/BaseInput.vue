<template>
  <div class="input-wrapper">
    <!-- Input -->
    <input
      :class="inputClass"
      :type="type"
      :value="modelValue"
      @input="updateValue"
      :disabled="disabled"
    />
    <!-- Hiển thị cảnh báo -->
    <div class="input-error-icon" v-if="errorMessage">
      <base-icon iconName="danger" :title="errorMessage" />
    </div>
  </div>
</template>

<script>
export default {
  props: ["type", "errorMessage", "modelValue", "disabled"],
  data() {
    return {
      inputClass: ["input", this.errorMessage ? "input-error" : null],
    };
  },
  methods: {
    /**
     * Bắt xự kiện người dùng nhập vào input
     * Author: linhpv (05/08/2022)
     * @param {*} event
     */
    updateValue(event) {
      this.$emit("update:modelValue", event.target.value);
      this.$emit("change", event.target.value);
    },
  },
};
</script>

<style scoped>
@import url(@/css/base/baseinput.css);
</style>