<template>
  <div class="popup-dish-bg">
    <div class="popup-dish">
      <div class="popup-dish-inner">
        <div class="popup-dish-header">
          <div>Thêm món</div>
          <div @click="toggleDishPopup">
            <base-icon iconName="x-close" />
          </div>
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
              <div class="pdi-1-left">
                <div class="form-input">
                  <div class="form-label">
                    Tên món <span style="color: red">(*)</span>
                  </div>
                  <base-input />
                </div>
                <div class="form-input">
                  <div class="form-label">
                    Mã món <span style="color: red">(*)</span>
                  </div>
                  <base-input />
                </div>
                <div class="form-input">
                  <div class="form-label">Nhóm thực đơn</div>
                  <base-combobox
                    :listItem="menuGroups"
                    tableName="MenuGroup"
                    :addIcon="true"
                  />
                </div>
                <div class="form-input">
                  <div class="form-label">
                    Đơn vị tính <span style="color: red">(*)</span>
                  </div>
                  <base-combobox
                    :listItem="units"
                    tableName="Unit"
                    :addIcon="true"
                  />
                </div>
                <div class="form-input">
                  <div class="form-label">Giá bán</div>
                  <base-input style="width: 113px" />
                </div>
                <div class="form-input">
                  <div class="form-label">Giá vốn</div>
                  <base-input style="width: 113px" />
                </div>
                <div class="form-input">
                  <div class="form-label">Mô tả</div>
                  <textarea class="textarea" rows="3"></textarea>
                </div>
                <div class="form-input">
                  <div class="form-label">Chế biến tại</div>
                  <base-combobox :listItem="kitchens" tableName="Kitchen" />
                </div>
                <div class="form-input">
                  <div class="form-label"></div>
                  <input id="checkbox" type="checkbox" style="display: none" />
                  <label for="checkbox" class="checkbox-label">
                    <div class="checkbox-icon"></div>
                    Không hiển thị trên thực đơn
                  </label>
                </div>
              </div>
              <div class="pdi-1-right">
                <fieldset>
                  <legend>Ảnh đại diện</legend>
                  <div style="display: flex; margin-top: 6px">
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
                      <input
                        id="input-image"
                        type="file"
                        style="display: none"
                        @change="handleChangeImage"
                        accept=".jpg,.jpeg,.png,.gif"
                      />
                      <label for="input-image" class="pdi-label-icon"
                        >...</label
                      >
                      <label class="pdi-label-icon">
                        <base-icon iconName="x-close-red" />
                      </label>
                    </div>
                  </div>
                </fieldset>
              </div>
            </div>
            <!-- Input tab 2 -->
            <div class="pdi-2" v-show="popupTab == 2">Tab 2 nay</div>
          </div>
          <div class="popup-dish-footer">
            <div class="pdf-left">
              <base-button content="Giúp" icon="question-circle" />
            </div>
            <div class="pdf-right">
              <base-button content="Cất" icon="save" />
              <base-button content="Cất & thêm" icon="save-add" />
              <base-button content="Hủy" icon="cancel" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";
import BaseIcon from "../../components/base/BaseIcon.vue";

export default {
  components: { BaseIcon },
  data() {
    return {
      popupTab: 1,
      fakeItem: [
        {
          fakeID: 1,
          fakeName: "a",
        },
      ],
    };
  },
  computed: mapState({
    menuGroups: (state) => state.dish.menuGroups,
    units: (state) => state.dish.units,
    kitchens: (state) => state.dish.kitchens,
  }),
  methods: {
    ...mapActions([
      "toggleDishPopup",
      "loadAllMenuGroup",
      "loadAllUnit",
      "loadAllKitchen",
    ]),
    /**
     * Preview ảnh
     * @param {*} e
     * Author: linhpv (10/08/2022)
     */
    handleChangeImage(e) {
      let imgPreview = this.$refs.imagePreview;
      URL.revokeObjectURL(imgPreview);
      const file = e.target.files[0];
      file.preview = URL.createObjectURL(file);
      imgPreview.src = file.preview;
    },
  },
  created() {
    this.loadAllMenuGroup();
    this.loadAllUnit();
    this.loadAllKitchen();
  },
};
</script>

<style scoped>
@import url(@/css/view/dishdetail.css);
</style>