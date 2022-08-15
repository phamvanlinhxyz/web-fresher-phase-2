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
      :maxlength="maxlength"
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
      maxlength: null,
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
     *  Định dạng các input giá tiền
     * @param {*} money
     * Author: linhpv (15/08/2022)
     */
    formatMoney(money) {
      if (typeof money === "number") {
        money = Math.round(money);
      }
      return money
        .toString()
        .replace(/\D/g, "")
        .replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    },
    /**
     * Xử lý hiển thị đầu vào input
     * @param {*} val
     * Author: linhpv (14/08/2022)
     */
    handleValue(val) {
      if (val && this.type == "money") {
        // return val.toLocaleString("vi-VN", {
        //   minimumFractionDigits: 2,
        //   maximumFractionDigits: 2,
        // });
        return this.formatMoney(val);
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
      if (this.type === "money") {
        event.target.value = this.formatMoney(event.target.value);
      }
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
    if (this.type == "money") {
      this.maxlength = 18;
    }
  },
};
</script>

<style scoped>
@import url(@/css/base/baseinput.css);
</style>