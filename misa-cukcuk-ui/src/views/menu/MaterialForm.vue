<template>
  <div class="unit-bg">
    <div class="unit-form">
      <div class="mgf-inner">
        <div class="popup-dish-header">
          <div>Thêm nguyên vật liệu</div>
          <div @click="toggleMaterialPopup">
            <base-icon iconName="x-close" />
          </div>
        </div>
        <div class="mgf-body">
          <div class="mgf-input">
            <div class="form-input">
              <div class="form-label">
                Mã <span style="color: red">(*)</span>
              </div>
              <base-input
                v-model="newMaterial.MaterialCode"
                @write="checkInputRequired('MaterialCode', $event.target.value)"
                :errorMessage="errorList.MaterialCode"
                :focus="focusElm === 'MaterialCode'"
              />
            </div>
            <div class="form-input">
              <div class="form-label">
                Tên <span style="color: red">(*)</span>
              </div>
              <base-input
                v-model="newMaterial.MaterialName"
                @write="checkInputRequired('MaterialName', $event.target.value)"
                :errorMessage="errorList.MaterialName"
                :focus="focusElm === 'MaterialName'"
              />
            </div>
            <div class="form-input">
              <div class="form-label">
                ĐVT <span style="color: red">(*)</span>
              </div>
              <base-combobox
                :listItem="units"
                tableName="Unit"
                :value="newMaterial.UnitID"
                :focus="focusElm == 'UnitID'"
                :errorMessage="errorList.UnitID"
                @change="(val) => this.setValueCombobox('Unit', val)"
              />
            </div>
            <div class="form-input">
              <div class="form-label">Ghi chú</div>
              <textarea
                class="textarea"
                rows="3"
                v-model="newMaterial.Note"
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
            <base-button
              content="Hủy"
              icon="cancel"
              @click="toggleMaterialPopup"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import resources from "@/resources";
import { mapActions, mapState } from "vuex";

export default {
  data() {
    return {
      newMaterial: {},
      errorList: {
        MaterialCode: null,
        MaterialName: null,
      },
      focusElm: "MaterialCode",
    };
  },
  computed: mapState({
    langCode: (state) => state.app.langCode,
    units: (state) => state.dish.units,
  }),
  methods: {
    ...mapActions(["toggleMaterialPopup", "loadAllUnit", "insertMaterial"]),
    /**
     * Thêm đơn vị tính mới
     * Author: linhpv (15/08/2022)
     */
    handleStoreUnit() {
      this.focusElm = undefined;
      if (this.validateData()) {
        this.insertMaterial(this.newMaterial);
      }
    },
    /**
     * Validate dữ liệu
     * Author: linhpv (16/08/2022)
     */
    validateData() {
      // Đặt valid mặc định true
      var valid = true;
      // Check đơn vị tính trống
      if (!this.newMaterial.MaterialCode) {
        if (!this.focusElm) {
          this.focusElm = "MaterialCode";
        }
        this.errorList.MaterialCode =
          resources.validateError[`${this.langCode}_Required_Error`];
        valid = false;
      }
      if (!this.newMaterial.MaterialName) {
        if (!this.focusElm) {
          this.focusElm = "MaterialName";
        }
        this.errorList.MaterialName =
          resources.validateError[`${this.langCode}_Required_Error`];
        valid = false;
      }
      if (!this.newMaterial.UnitID) {
        if (!this.focusElm) {
          this.focusElm = "UnitID";
        }
        this.errorList.UnitID =
          resources.validateError[`${this.langCode}_Required_Error`];
        valid = false;
      }

      return valid;
    },
    /**
     * Check input required
     * Author: linhpv (16/08/2022)
     */
    checkInputRequired(column, value) {
      if (!value) {
        this.errorList[column] =
          resources.validateError[`${this.langCode}_Required_Error`];
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
        this.errorList[`${tableName}ID`] =
          resources.validateError[`${this.langCode}_Required_Error`];
        this.newMaterial[`${tableName}ID`] = null;
        this.newMaterial[`${tableName}Name`] = null;
      } else {
        this.errorList[`${tableName}ID`] = null;
        this.newMaterial[`${tableName}ID`] = value[`${tableName}ID`];
        this.newMaterial[`${tableName}Name`] = value[`${tableName}Name`];
      }
    },
  },
  created() {
    this.loadAllUnit();
  },
};
</script>

<style scoped>
@import url(@/css/view/menu/menugroupform.css);
</style>