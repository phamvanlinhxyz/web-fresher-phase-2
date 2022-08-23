<template>
  <div class="dialog-bg">
    <div class="dialog-form">
      <div class="mgf-inner">
        <div class="popup-dish-header">
          <div>CUKCUK - Quản lý nhà hàng</div>
        </div>
        <div class="dialog-body">
          <div class="dialog-icon">
            <base-icon :iconName="dialogIcon" />
          </div>
          <div class="dialog-message">
            <!-- Nếu message là tring -->
            <div v-if="typeof message == 'string'">
              {{ message }}
            </div>
            <!-- Nếu masage là array -->
            <ul v-if="typeof message == 'object'">
              <li v-for="(msg, index) of message" :key="index">{{ msg }}</li>
            </ul>
          </div>
        </div>
        <div class="dialog-footer">
          <base-button
            :content="button.content"
            v-for="(button, index) of rightButton"
            :key="index"
            @click="handleButtonClick(button.type)"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dialogIcon: "",
    };
  },
  props: ["type", "message", "rightButton"],
  methods: {
    /**
     * Xử lý sự kiện click
     * @param {*} buttonType
     */
    handleButtonClick(buttonType) {
      // Gửi emit lên thằng cha
      this.$emit(`${buttonType}`);
    },
  },
  created() {
    this.dialogIcon = this.type ? `${this.type}-32` : "question-32";
  },
};
</script>

<style scoped>
@import url(@/css/base/basedialog.css);
</style>