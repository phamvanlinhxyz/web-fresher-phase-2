<template>
  <div class="unit-bg">
    <div class="kitchen-form">
      <div class="mgf-inner">
        <div class="popup-dish-header">
          <div>Thêm bếp</div>
          <div @click="handleClosePopup">
            <base-icon iconName="x-close" />
          </div>
        </div>
        <div class="mgf-body">
          <div class="mgf-input">
            <div class="form-input">
              <div class="form-label">
                Tên bếp <span style="color: red">(*)</span>
              </div>
              <base-input
                v-model="newKitchen.KitchenName"
                @write="checkInputRequired('KitchenName', $event.target.value)"
                :errorMessage="errorList.KitchenName"
                :focus="focusElm === 'KitchenName'"
              />
            </div>
            <div class="form-input">
              <div class="form-label">Sử dụng thiết bị</div>
              <div class="radio-group">
                <div class="radio">
                  <input
                    id="tablet"
                    type="radio"
                    class="input-radio"
                    v-model="newKitchen.Device"
                    :value="1"
                  />
                  <label for="tablet">
                    <base-icon iconName="radio" />Máy tính bảng
                  </label>
                </div>
                <div class="radio">
                  <input
                    id="printer"
                    type="radio"
                    class="input-radio"
                    v-model="newKitchen.Device"
                    :value="2"
                  />
                  <label for="printer">
                    <base-icon iconName="radio" />Máy in
                  </label>
                </div>
                <div class="radio">
                  <input
                    id="no"
                    type="radio"
                    class="input-radio"
                    v-model="newKitchen.Device"
                    :value="0"
                  />
                  <label for="no">
                    <base-icon iconName="radio" />Không sử dụng
                  </label>
                </div>
              </div>
            </div>
            <div class="form-input">
              <div class="form-label">Diễn giải</div>
              <textarea
                class="textarea"
                rows="3"
                v-model="newKitchen.Description"
              ></textarea>
            </div>
          </div>
        </div>
        <div class="mgf-footer">
          <div class="pdf-left">
            <base-button content="Giúp" icon="question-circle" />
          </div>
          <div class="pdf-right">
            <base-button
              content="Cất"
              icon="save"
              @click="handleStoreKitchen"
            />
            <base-button
              content="Hủy"
              icon="cancel"
              @click="toggleKitchenPopup"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
  <!-- Dialog confirm đóng popup -->
  <base-dialog
    v-if="isShowConfirmDialog"
    :message="dialogMsg"
    :rightButton="[
      { content: 'Có', type: 'confirm' },
      { content: 'Không', type: 'no-confirm' },
      { content: 'Hủy bỏ', type: 'cancel' },
    ]"
    @confirm="
      () => {
        this.isShowConfirmDialog = false;
        this.handleStoreKitchen();
      }
    "
    @no-confirm="toggleKitchenPopup"
    @cancel="() => (this.isShowConfirmDialog = false)"
  />
</template>

<script>
import resources from "@/resources";
import { mapActions, mapState } from "vuex";
import { objectEqual } from "@/utils";

export default {
  data() {
    return {
      newKitchen: {
        Device: 2,
      },
      errorList: {
        KitchenName: null,
      },
      focusElm: "KitchenName",
      isShowConfirmDialog: false,
      dialogMsg: "",
    };
  },
  computed: mapState({
    langCode: (state) => state.app.langCode,
  }),
  methods: {
    /**
     * Người dùng ấn đóng popup
     * Author: linhpv (22/08/2022)
     */
    handleClosePopup() {
      if (objectEqual(this.newKitchen, {})) {
        this.toggleKitchenPopup();
      } else {
        this.dialogMsg = resources[`${this.langCode}_Dialog_Msg`].dataChanged;
        this.isShowConfirmDialog = true;
      }
    },
    ...mapActions(["toggleKitchenPopup", "insertKitchen"]),
    /**
     * Thêm đơn vị tính mới
     * Author: linhpv (22/08/2022)
     */
    handleStoreKitchen() {
      console.log(this.newKitchen);
      this.focusElm = undefined;
      if (this.validateData()) {
        this.insertKitchen(this.newKitchen);
      }
    },
    /**
     * Validate dữ liệu
     * @returns true - valid, false - không valid
     * Author: linhpv (22/08/2022)
     */
    validateData() {
      // Đặt valid mặc định true
      var valid = true;
      // Check đơn vị tính trống
      if (!this.newKitchen.KitchenName) {
        if (!this.focusElm) {
          this.focusElm = "KitchenName";
        }
        this.errorList.KitchenName =
          resources[`${this.langCode}_Error_Msg`].emptyName;
        valid = false;
      }

      return valid;
    },
    /**
     * Check input required
     * Author: linhpv (15/08/2022)
     */
    checkInputRequired(column, value) {
      if (!value) {
        this.errorList[column] =
          resources[`${this.langCode}_Error_Msg`].required;
      } else {
        this.errorList[column] = null;
      }
    },
  },
};
</script>

<style scoped>
@import url(@/css/view/menu/menugroupform.css);
</style>