<template>
  <div class="menu-group-bg">
    <div class="menu-group-form">
      <div class="mgf-inner">
        <div class="popup-dish-header">
          <div>Thêm nhóm thực đơn</div>
          <div @click="handleClosePopup">
            <base-icon iconName="x-close" />
          </div>
        </div>
        <div class="mgf-body">
          <div class="mgf-input">
            <div class="form-input">
              <div class="form-label">
                Mã nhóm <span style="color: red">(*)</span>
              </div>
              <base-input
                v-model="newMenuGroup.MenuGroupCode"
                @write="
                  checkInputRequired('MenuGroupCode', $event.target.value)
                "
                :errorMessage="errorList.MenuGroupCode"
                :focus="focusElm == 'MenuGroupCode'"
              />
            </div>
            <div class="form-input">
              <div class="form-label">
                Tên nhóm <span style="color: red">(*)</span>
              </div>
              <base-input
                v-model="newMenuGroup.MenuGroupName"
                @write="
                  checkInputRequired('MenuGroupName', $event.target.value)
                "
                :errorMessage="errorList.MenuGroupName"
                :focus="focusElm == 'MenuGroupName'"
              />
            </div>
            <div class="form-input">
              <div class="form-label">Chế biến tại</div>
              <base-combobox
                :listItem="kitchens"
                tableName="Kitchen"
                :value="newMenuGroup.KitchenID"
                @change="(val, tab) => this.setValueCombobox('Kitchen', val)"
              />
            </div>
            <div class="form-input">
              <div class="form-label">Diễn giải</div>
              <textarea
                class="textarea"
                rows="3"
                v-model="newMenuGroup.Description"
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
              @click="handleStoreMenuGroup"
            />
            <base-button
              content="Hủy"
              icon="cancel"
              @click="toggleMenuGroupPopup"
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
        this.handleStoreMenuGroup();
      }
    "
    @no-confirm="toggleMenuGroupPopup"
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
      errorList: {
        MenuGroupCode: null,
        MenuGroupName: null,
      },
      newMenuGroup: {},
      focusElm: "MenuGroupCode",
      isShowConfirmDialog: false,
      dialogMsg: "",
    };
  },
  computed: mapState({
    langCode: (state) => state.app.langCode,
    kitchens: (state) => state.kitchen.kitchens,
  }),
  methods: {
    ...mapActions([
      "loadAllKitchen",
      "toggleMenuGroupPopup",
      "insertMenuGroup",
    ]),
    /**
     * Người dùng ấn đóng popup
     * Author: linhpv (19/08/2022)
     */
    handleClosePopup() {
      if (objectEqual(this.newMenuGroup, {})) {
        this.toggleMenuGroupPopup();
      } else {
        this.dialogMsg = resources[`${this.langCode}_Dialog_Msg`].dataChanged;
        this.isShowConfirmDialog = true;
      }
    },
    /**
     * Thêm nhóm thực đơn mới
     * Author: linhpv (15/08/2022)
     */
    handleStoreMenuGroup() {
      this.focusElm = null;
      let valid = this.validateData();
      if (valid) {
        this.insertMenuGroup(this.newMenuGroup);
      }
    },
    /**
     * Validate dữ liệu
     * Author: linhpv (15/08/2022)
     */
    validateData() {
      // Đặt valid mặc định là true
      var valid = true;
      // Kiểm tra các trường bắt buộc
      // 1. Mã nhóm thực đơn
      if (!this.newMenuGroup.MenuGroupCode) {
        if (!this.focusElm) {
          this.focusElm = "MenuGroupCode";
        }
        this.errorList.MenuGroupCode =
          resources[`${this.langCode}_Error_Msg`].emptyCode;
        valid = false;
      }
      // 2. Tên nhóm thực đơn
      if (!this.newMenuGroup.MenuGroupName) {
        if (!this.focusElm) {
          this.focusElm = "MenuGroupName";
        }
        this.errorList.MenuGroupName =
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
    /**
     * Set giá trị cho các cột lấy từ combobox
     * @param {string} tableName tên bảng
     * @param {object} value giá trị
     * Author: linhpv (16/08/2022)
     */
    setValueCombobox(tableName, value) {
      if (!value) {
        this.newMenuGroup[`${tableName}ID`] = null;
        this.newMenuGroup[`${tableName}Name`] = null;
      } else {
        this.newMenuGroup[`${tableName}ID`] = value[`${tableName}ID`];
        this.newMenuGroup[`${tableName}Name`] = value[`${tableName}Name`];
      }
    },
  },
  async created() {
    await this.loadAllKitchen();
  },
};
</script>

<style scoped>
@import url(@/css/view/menu/menugroupform.css);
</style>