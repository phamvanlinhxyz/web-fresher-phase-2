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
                        ref="imagePreview"
                        src="https://misathai.cukcuk.com/Handler/ImageHandler.ashx?FileType=1&IsTemp=True&W=160&H=120&IsFit=true"
                        width="160"
                        height="120"
                      />
                      <div class="pdi-image-text">
                        Chọn các ảnh có định dạng
                        <b>(.jpg, .jpeg, .png, .gif)</b>
                      </div>
                    </div>
                    <div class="pdi-label">
                      <input id="input-image" type="file" style="display:none" @change="handleChangeImage" accept=".jpg,.jpeg,.png,.gif" />
                      <label for="input-image" class="pdi-label-icon">...</label>
                      <label class="pdi-label-icon">x</label>
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
import { mapState } from 'vuex';
export default {
  data() {
    return {
      popupTab: 1,
    };
  },
  methods: {
    handleChangeImage(e) {
      let imgPreview = this.$refs.imagePreview
      URL.revokeObjectURL(imgPreview);
      const file = e.target.files[0];
      file.preview = URL.createObjectURL(file);
      imgPreview.src = file.preview;
    }
  },
};
</script>

<style scoped>
@import url(@/css/view/dishdetail.css);
</style>