<template>
  <div class="unit-bg">
    <div class="unit-form">
      <div class="mgf-inner">
        <div class="popup-dish-header">
          <div>Thêm đơn vị tính</div>
          <div @click="handleClosePopup">
            <base-icon iconName="x-close" />
          </div>
        </div>
        <div class="mgf-body">
          <div class="mgf-input">
            <div class="form-input">
              <div class="form-label">
                Đơn vị tính <span style="color: red">(*)</span>
              </div>
              <base-input
                v-model="newUnit.UnitName"
                @write="checkInputRequired('UnitName', $event.target.value)"
                :errorMessage="errorList.UnitName"
                :focus="focusElm === 'UnitName'"
              />
            </div>
            <div class="form-input">
              <div class="form-label">Diễn giải</div>
              <textarea
                class="textarea"
                rows="3"
                v-model="newUnit.Description"
              ></textarea>
            </div>
          </div>
        </div>
        <div class="mgf-footer">
          <div class="pdf-left">
            <base-button content="Giúp" icon="question-circle" />
          </div>
          <div class="pdf-right">
            <base-button content="Cất" icon="save" @click="handleStoreUnit" />
            <base-button content="Hủy" icon="cancel" @click="toggleUnitPopup" />
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
        this.handleStoreUnit();
      }
    "
    @no-confirm="toggleUnitPopup"
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
      newUnit: {},
      errorList: {
        UnitName: null,
      },
      focusElm: "UnitName",
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
     * Author: linhpv (19/08/2022)
     */
    handleClosePopup() {
      if (objectEqual(this.newUnit, {})) {
        this.toggleUnitPopup();
      } else {
        this.dialogMsg = resources[`${this.langCode}_Dialog_Msg`].dataChanged;
        this.isShowConfirmDialog = true;
      }
    },
    ...mapActions(["toggleUnitPopup", "insertUnit"]),
    /**
     * Thêm đơn vị tính mới
     * Author: linhpv (15/08/2022)
     */
    handleStoreUnit() {
      this.focusElm = undefined;
      if (this.validateData()) {
        this.insertUnit(this.newUnit);
      }
    },
    validateData() {
      // Đặt valid mặc định true
      var valid = true;
      // Check đơn vị tính trống
      if (!this.newUnit.UnitName) {
        if (!this.focusElm) {
          this.focusElm = "UnitName";
        }
        this.errorList.UnitName =
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