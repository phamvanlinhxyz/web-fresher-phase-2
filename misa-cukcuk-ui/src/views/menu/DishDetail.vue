<template>
  <div class="popup-dish-bg">
    <div class="popup-dish">
      <div class="popup-dish-inner">
        <div class="popup-dish-header">
          <div>
            {{ formTitle }}
          </div>
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
                  <base-input
                    v-model="singleDish.DishName"
                    @changeValue="getNewCode"
                    @write="checkInputRequired('DishName', $event.target.value)"
                    :errorMessage="errorList.DishName"
                    :focus="focusElm === 'DishName'"
                  />
                </div>
                <div class="form-input">
                  <div class="form-label">
                    Mã món <span style="color: red">(*)</span>
                  </div>
                  <base-input
                    v-model="singleDish.DishCode"
                    @write="checkInputRequired('DishCode', $event.target.value)"
                    :errorMessage="errorList.DishCode"
                    :focus="focusElm === 'DishCode'"
                  />
                </div>
                <div class="form-input">
                  <div class="form-label">Nhóm thực đơn</div>
                  <base-combobox
                    :listItem="menuGroups"
                    tableName="MenuGroup"
                    :addIcon="true"
                    :value="singleDish.MenuGroupID"
                    @change="
                      (val) => this.checkComboboxRequired('MenuGroup', val)
                    "
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
                    :value="singleDish.UnitID"
                    @change="(val) => this.checkComboboxRequired('Unit', val)"
                    :focus="focusElm == 'UnitID'"
                    :errorMessage="errorList.UnitID"
                  />
                </div>
                <div class="form-input">
                  <div class="form-label">
                    Giá bán <span style="color: red">(*)</span>
                  </div>
                  <base-input
                    type="money"
                    style="width: 113px"
                    v-model="singleDish.Price"
                    @write="checkInputRequired('Price', $event.target.value)"
                    :errorMessage="errorList.Price"
                    :focus="focusElm == 'Price'"
                  />
                </div>
                <div class="form-input">
                  <div class="form-label">Giá vốn</div>
                  <base-input
                    type="money"
                    style="width: 113px"
                    v-model="singleDish.PurchasePrice"
                  />
                </div>
                <div class="form-input">
                  <div class="form-label">Mô tả</div>
                  <textarea
                    class="textarea"
                    rows="3"
                    v-model="singleDish.Description"
                  ></textarea>
                </div>
                <div class="form-input">
                  <div class="form-label">Chế biến tại</div>
                  <base-combobox
                    :listItem="kitchens"
                    tableName="Kitchen"
                    :value="singleDish.KitchenID"
                    @change="
                      (val) => this.checkComboboxRequired('Kitchen', val)
                    "
                  />
                </div>
                <div class="form-input">
                  <div class="form-label"></div>
                  <input
                    id="checkbox"
                    type="checkbox"
                    style="display: none"
                    v-model="singleDish.ShowOnMenu"
                    :true-value="0"
                    :false-value="1"
                  />
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
              <base-button content="Cất" icon="save" @click="handleStore" />
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
import axios from "axios";
import { resources } from "@/resources";
import enums from "@/enums";
import { constants } from "@/config";
import { mapActions, mapState } from "vuex";
import BaseIcon from "../../components/base/BaseIcon.vue";

export default {
  components: { BaseIcon },
  data() {
    return {
      popupTab: 1,
      singleDish: {},
      errorList: {
        DishName: null,
        DishCode: null,
        UnitID: null,
        Price: null,
      },
      focusElm: "DishName",
      formTitle: "Thêm món",
    };
  },
  computed: mapState({
    langCode: (state) => state.app.langCode,
    menuGroups: (state) => state.dish.menuGroups,
    units: (state) => state.dish.units,
    kitchens: (state) => state.dish.kitchens,
    selectedDish: (state) => state.dish.selectedDish,
    formMode: (state) => state.dish.formMode,
  }),
  methods: {
    ...mapActions([
      "toggleDishPopup",
      "loadAllMenuGroup",
      "loadAllUnit",
      "loadAllKitchen",
      "insertDish",
    ]),
    /**
     * Người dùng ấn cất
     * Author: linhpv (12/08/2022)
     */
    handleStore() {
      this.focusElm = "";
      var valid = this.validateData();
      if (valid) {
        // Format lại các định dạng giá tiền
        this.formatData();
        // Check formMode
        if (this.formMode == enums.formMode.Add) {
          this.insertDish(this.singleDish);
        } else if (this.formMode == enums.formMode.Edit) {
          console.log(this.singleDish);
        }
      }
    },
    /**
     * Format lại một số dữ liệu cần thiết
     * Author: linhpv (15/08/2022)
     */
    formatData() {
      // 1. Xử lý giá bán
      if (this.singleDish.Price && typeof this.singleDish.Price == "string") {
        this.singleDish.Price = parseFloat(
          this.singleDish.Price.replaceAll(".", "")
        );
      }
      // 2. Xử lý giá vốn
      if (
        this.singleDish.PurchasePrice &&
        typeof this.singleDish.PurchasePrice == "string"
      ) {
        this.singleDish.PurchasePrice = parseFloat(
          this.singleDish.PurchasePrice(".", "")
        );
      }
    },
    /**
     * Validate dữ liệu
     * Author: linhpv (14/08/2022)
     */
    validateData() {
      // Đặt valid mặc định là true
      var valid = true;
      // Check các dữ liệu bắt buộc
      // 1. Tên món ăn
      if (!this.singleDish.DishName) {
        if (!this.focusElm) {
          this.focusElm = "DishName";
        }
        this.errorList.DishName =
          resources.validateError[`${this.langCode}_Required_Error`];
        valid = false;
      }
      // 2. Mã món ăn
      if (!this.singleDish.DishCode) {
        if (!this.focusElm) {
          this.focusElm = "DishCode";
        }
        this.errorList.DishCode =
          resources.validateError[`${this.langCode}_Required_Error`];
        valid = false;
      }
      // 3. Đơn vị tính
      if (!this.singleDish.UnitID) {
        if (!this.focusElm) {
          this.focusElm = "UnitID";
        }
        this.errorList.UnitID =
          resources.validateError[`${this.langCode}_Required_Error`];
        valid = false;
      }
      // 4. Giá bán
      if (!this.singleDish.Price) {
        if (!this.focusElm) {
          this.focusElm = "Price";
        }
        this.errorList.Price =
          resources.validateError[`${this.langCode}_Required_Error`];
        valid = false;
      }
      // Gán tạm cho định lượng NVL = 0;
      this.singleDish.MaterialQuantified = enums.yesNo.No;
      return valid;
    },
    /**
     * Set giá trị cho các cột lấy từ combobox
     * @param {*} tableName
     * @param {*} value
     * Author: linhpv (14/08/2022)
     */
    setValueCombobox(tableName, value) {
      if (!value) {
        this.singleDish[`${tableName}ID`] = null;
        this.singleDish[`${tableName}Name`] = null;
      } else {
        this.singleDish[`${tableName}ID`] = value[`${tableName}ID`];
        this.singleDish[`${tableName}Name`] = value[`${tableName}Name`];
      }
    },
    /**
     * Check các combobox required
     * @param {*} column
     * @param {*} value
     * Author: linhpv (14/08/2022)
     */
    checkComboboxRequired(tableName, value) {
      if (!value) {
        this.errorList[`${tableName}ID`] =
          resources.validateError[`${this.langCode}_Required_Error`];
      } else {
        this.errorList[`${tableName}ID`] = null;
      }
      this.setValueCombobox(tableName, value);
    },
    /**
     * Check input required
     * Author: linhpv (13/08/2022)
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
     * Sinh mã cho món ăn
     * @param {*} val
     * Author: linhpv (12/08/2022)
     */
    async getNewCode(val) {
      // Nếu có giá trị thì mới gọi API lấy mã mới
      if (val) {
        const res = await axios.get(
          `${constants.API_URL}/api/${constants.API_VERSION}/Dish/NewCode?DishName=${val}`
        );
        if (res.data.Success) {
          this.singleDish.DishCode = res.data.Data;
          this.errorList.DishCode = null;
        }
      }
    },
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
  /**
   * Xử lý các xự kiện khi khởi tạo component
   * Author: linhpv (12/08/2022)
   */
  created() {
    this.loadAllMenuGroup();
    this.loadAllUnit();
    this.loadAllKitchen();
    this.singleDish = this.selectedDish;
  },
  /**
   * Xử lý các sự kiện khi mount component
   * Author: linhpv (14/08/2022)
   */
  mounted() {
    if (this.formMode == enums.formMode.Add) {
      this.formTitle = "Thêm món";
    } else if (this.formMode == enums.formMode.Edit) {
      this.formTitle = "Sửa món";
    }
  },
};
</script>

<style scoped>
@import url(@/css/view/dishdetail.css);
</style>