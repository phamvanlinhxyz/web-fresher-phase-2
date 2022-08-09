<template>
  <div class="popup-dish-bg">
    <div class="popup-dish">
      <div class="popup-dish-inner">
        <div class="popup-dish-header">
          <div>Thêm món</div>
          <div>x</div>
        </div>
        <div class="popup-dish-body">
          <div class="popup-dish-tab">
            <div
              class="pdt-option"
              :class="{ selected: popupTab == 1 }"
              @click="this.popupTab = 1"
            >
              Thông tin chung
            </div>
            <div
              class="pdt-option"
              :class="{ selected: popupTab == 2 }"
              @click="this.popupTab = 2"
            >
              Định lượng nguyên vật liệu
            </div>
          </div>
          <div class="popup-dish-input">
            <!-- Input tab 1 -->
            <div class="pdi-1" v-show="popupTab == 1">
              <div class="pdi-1-left"></div>
              <div class="pdi-1-right">
                <fieldset>
                  <legend>Ảnh đại diện</legend>
                  <div style="display: flex">
                    <div class="pdi-image">
                      <img
                        src="https://misathai.cukcuk.com/Handler/ImageHandler.ashx?FileType=1&IsTemp=True&W=160&H=120&IsFit=true"
                      />
                      <div class="pdi-image-text">
                        Chọn các ảnh có định dạng
                        <b>(.jpg, .jpeg, .png, .gif)</b>
                      </div>
                    </div>
                    <div class="pdi-label">
                      <div class="pdi-label-icon">...</div>
                      <div class="pdi-label-icon">x</div>
                    </div>
                  </div>
                </fieldset>
              </div>
            </div>
            <!-- Input tab 2 -->
            <div class="pdi-2" v-show="popupTab == 2">Tab 2 nay</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      popupTab: 1,
    };
  },
};
</script>

<style scoped>
@import url(@/css/view/dishdetail.css);
</style>