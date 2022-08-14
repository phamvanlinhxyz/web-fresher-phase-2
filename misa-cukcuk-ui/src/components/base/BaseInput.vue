<template>
  <div class="input-wrapper">
    <!-- Input -->
    <input
      :class="inputClass"
      :type="type"
      :value="handleValue(modelValue)"
      :disabled="disabled"
      @input="updateValue"
      @change="changeValue"
      ref="inputRef"
    />
    <!-- Hiển thị cảnh báo -->
    <div class="input-error-icon" v-if="errorMessage">
      <base-icon iconName="danger" :title="errorMessage" />
    </div>
  </div>
</template>

<script>
export default {
  props: ["type", "errorMessage", "modelValue", "disabled", "focus"],
  data() {
    return {
      inputClass: ["input", this.errorMessage ? "input-error" : null],
    };
  },
  watch: {
    errorMessage(newVal) {
      if (newVal == null) {
        this.inputClass = ["input"];
      } else {
        this.inputClass = ["input", this.errorMessage ? "input-error" : null];
      }
    },
    focus(newVal) {
      if (newVal) {
        this.$refs.inputRef.focus();
      }
    },
  },
  methods: {
    /**
     * Xử lý hiển thị đầu vào input
     * @param {*} val
     * Author: linhpv (14/08/2022)
     */
    handleValue(val) {
      if (val && this.type == "money") {
        return val.toLocaleString("vi-VN", {
          minimumFractionDigits: 2,
          maximumFractionDigits: 2,
        });
      } else {
        return val;
      }
    },
    /**
     * Bắt xự kiện người dùng nhập vào input
     * Author: linhpv (05/08/2022)
     * @param {*} event
     */
    updateValue(event) {
      this.$emit("update:modelValue", event.target.value);
      this.$emit("write", event);
    },
    /**
     * Bắt sự kiện thay đổi giá trị khi người dùng hoàn thành nhập
     * @param {*} event
     * Author: linhpv (05/08/2022)
     */
    changeValue(event) {
      this.$emit("changeValue", event.target.value);
    },
  },
  mounted() {
    if (this.focus) {
      this.$refs.inputRef.focus();
    }
  },
};
</script>

<style scoped>
@import url(@/css/base/baseinput.css);
</style>