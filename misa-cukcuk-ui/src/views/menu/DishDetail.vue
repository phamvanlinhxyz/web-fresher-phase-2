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
                    @change="(val) => this.setValueCombobox('MenuGroup', val)"
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
                    @change="(val) => this.setValueCombobox('Kitchen', val)"
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
            <div class="pdi-2" v-show="popupTab == 2">
              <div class="pdi-2-header">
                <div>Món ăn</div>
                <div class="pdi-2-info">
                  <base-icon iconName="info" />
                  <div class="pdi-2-message">
                    Giá vốn được tự động cập nhật theo giá vốn các NVL thành
                    phần.
                  </div>
                </div>
              </div>
              <div class="pdi-2-body">
                <table class="pdi-table">
                  <thead>
                    <tr>
                      <td style="width: 108px">
                        <div>Mã NVL</div>
                      </td>
                      <td style="width: 214px"><div>Nguyên vật liệu</div></td>
                      <td style="width: 100px"><div>Đơn vị tính</div></td>
                      <td style="width: 100px"><div>Số lượng</div></td>
                      <td style="width: 100px"><div>Giá vốn</div></td>
                      <td style="width: 100px"><div>Thành tiền</div></td>
                    </tr>
                  </thead>
                  <tbody>
                    <tr
                      v-for="(material, index) of dishMaterial"
                      :key="index"
                      @click="handleClickRow(index)"
                    >
                      <td>
                        <base-combobox
                          :listItem="materials"
                          tableName="Material"
                          :addIcon="true"
                          :hideBorder="true"
                          :dropdownType="1"
                          columnShow="MaterialCode"
                          :value="material.MaterialID"
                          @change="handleChangeMaterial"
                        />
                      </td>
                      <td>
                        <base-input
                          :hideBorder="true"
                          :disabled="true"
                          v-model="material.MaterialName"
                        />
                      </td>
                      <td>
                        <base-input
                          :hideBorder="true"
                          :disabled="true"
                          v-model="material.UnitName"
                        />
                      </td>
                      <td>
                        <base-input
                          :hideBorder="true"
                          type="money"
                          v-model="material.MaterialAmount"
                          @change="calcTotalPrice"
                        />
                      </td>
                      <td>
                        <base-input
                          :hideBorder="true"
                          type="money"
                          v-model="material.MaterialPurchasePrice"
                          @change="calcTotalPrice"
                        />
                      </td>
                      <td>
                        <base-input
                          :hideBorder="true"
                          :disabled="true"
                          type="money"
                          v-model="material.TotalPrice"
                        />
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
              <div class="pdi-2-footer">
                <base-button
                  content="Thêm dòng"
                  icon="insert"
                  @click="addRowMaterial"
                />
                <base-button
                  content="Xóa dòng"
                  icon="x-close-red"
                  @click="handleDeleteRow"
                  :disabled="dishMaterial.length == 0"
                />
              </div>
            </div>
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
import resources from "@/resources";
import enums from "@/enums";
import { constants } from "@/config";
import { mapActions, mapState } from "vuex";

export default {
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
      dishMaterial: [],
      dishMaterialFocus: 0,
      defaultMaterial: {
        MaterialAmount: 1,
        MaterialPurchasePrice: 0,
        TotalPrice: 0,
      },
    };
  },
  computed: mapState({
    langCode: (state) => state.app.langCode,
    menuGroups: (state) => state.dish.menuGroups,
    units: (state) => state.dish.units,
    kitchens: (state) => state.dish.kitchens,
    materials: (state) => state.dish.materials,
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
      "loadAllMaterial",
      "updateDish",
    ]),
    /**
     * Lấy định lượng nguyên vật liệu
     * @param {string} dishID Id món ăn
     * Author: linhpv (17/08/2022)
     */
    async loadDishMaterial(dishID) {
      const res = await axios.get(
        `${constants.API_URL}/api/${constants.API_VERSION}/Dish/ListMaterial?dishID=${dishID}`
      );
      if (res.data.Success) {
        this.dishMaterial = res.data.Data;
      }
    },
    /**
     * Tính toán thành tiền
     * Author: linhpv (16/08/2022)
     */
    calcTotalPrice() {
      // Lấy ra nguyên vật liệu đang tính
      let material = this.dishMaterial[this.dishMaterialFocus];
      // Nếu người dùng không nhập số lượng => set số lượng là 0
      if (!material.MaterialAmount) {
        material.MaterialAmount = 0;
        material.TotalPrice = 0;
        return;
      }
      // Nếu người dùng không nhập giá vốn => set giá vốn = 0
      if (!material.MaterialPurchasePrice) {
        material.MaterialPurchasePrice = 0;
        material.TotalPrice = 0;
        return;
      }
      material.TotalPrice =
        parseFloat(material.MaterialAmount.replaceAll(".", "")) *
        parseFloat(material.MaterialPurchasePrice.replaceAll(".", ""));
    },
    /**
     * Xóa 1 dòng nguyên vật liệu
     * Author: linhpv (16/08/2022)
     */
    handleDeleteRow() {
      // Nếu số lượng khác không thì xóa
      if (this.dishMaterial.length > 0) {
        this.dishMaterial.splice(this.dishMaterialFocus, 1);
        // Nếu focus nằm ngoài số lượng nguyên vật liệu => chọn cái cuối
        if (this.dishMaterialFocus >= this.dishMaterial.length) {
          this.dishMaterialFocus = this.dishMaterial.length - 1;
        }
      }
    },
    /**
     * Sự kiện người dùng ấn chọn dòng
     * @param {int} index vị trí index của dòng click
     * Author: linhpv (16/08/2022)
     */
    handleClickRow(index) {
      this.dishMaterialFocus = index;
    },
    /**
     * Thêm một hàng nguyê/n vật liệu
     * Author: linhpv (16/08/2022)
     */
    addRowMaterial() {
      // Thêm một dòng mới
      this.dishMaterial.push(JSON.parse(JSON.stringify(this.defaultMaterial)));
      // Set focus mặc định vào dòng mới thêm
      this.dishMaterialFocus = this.dishMaterial.length - 1;
    },
    /**
     * Người dúng chọn nguyên vật liệu khác
     * @param {*} material
     * Author: linhpv (16/08/2022)
     */
    handleChangeMaterial(material) {
      this.dishMaterial[this.dishMaterialFocus].MaterialID =
        material.MaterialID;
      this.dishMaterial[this.dishMaterialFocus].MaterialName =
        material.MaterialName;
      this.dishMaterial[this.dishMaterialFocus].UnitName = material.UnitName;
    },
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
        // Xử lý định lượng nguyên vật liệu
        this.handleDishMaterial();
        // Check formMode
        if (this.formMode == enums.formMode.Add) {
          this.insertDish(this.singleDish);
        } else if (this.formMode == enums.formMode.Edit) {
          this.updateDish(this.singleDish);
          // console.log(this.singleDish);
        }
      }
    },
    /**
     * Xử lý định lượng nguyên vật liệu
     * Author: linhpv (17/08/2022)
     */
    handleDishMaterial() {
      // Kiểm tra các định lượng có nguyên vật liệu chưa, nếu chưa => xóa
      this.dishMaterial = this.dishMaterial.filter((dm) => {
        return dm.MaterialID;
      });
      // Set focus vào thằng cuối cùng
      this.dishMaterialFocus = this.dishMaterial.length - 1;
      // Xử lý định dạng số
      this.dishMaterial.forEach((dm) => {
        if (typeof dm.MaterialAmount === "string") {
          dm.MaterialAmount = parseFloat(dm.MaterialAmount.replaceAll(".", ""));
        }
        if (typeof dm.MaterialPurchasePrice === "string") {
          dm.MaterialPurchasePrice = parseFloat(
            dm.MaterialPurchasePrice.replaceAll(".", "")
          );
        }
      });
      // Thêm định lượng nguyên vật liệu vào món ăn
      this.singleDish.DishMaterials = this.dishMaterial;
      // Thay đổi thuộc tính định lượng nguyên vật liệu
      if (
        this.singleDish.DishMaterials &&
        this.singleDish.DishMaterials.length > 0
      ) {
        this.singleDish.MaterialQuantified = enums.yesNo.Yes;
      } else {
        this.singleDish.MaterialQuantified = enums.yesNo.No;
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
          this.singleDish.PurchasePrice.replaceAll(".", "")
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
        this.errorList.DishName = resources[`${this.langCode}_Required_Error`];
        valid = false;
      }
      // 2. Mã món ăn
      if (!this.singleDish.DishCode) {
        if (!this.focusElm) {
          this.focusElm = "DishCode";
        }
        this.errorList.DishCode = resources[`${this.langCode}_Required_Error`];
        valid = false;
      }
      // 3. Đơn vị tính
      if (!this.singleDish.UnitID) {
        if (!this.focusElm) {
          this.focusElm = "UnitID";
        }
        this.errorList.UnitID = resources[`${this.langCode}_Required_Error`];
        valid = false;
      }
      // 4. Giá bán
      if (!this.singleDish.Price) {
        if (!this.focusElm) {
          this.focusElm = "Price";
        }
        this.errorList.Price = resources[`${this.langCode}_Required_Error`];
        valid = false;
      }
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
          resources[`${this.langCode}_Required_Error`];
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
        this.errorList[column] = resources[`${this.langCode}_Required_Error`];
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
      URL.revokeObjectURL(imgPreview.src);
      const file = e.target.files[0];
      imgPreview.src = URL.createObjectURL(file);
    },
  },
  /**
   * Xử lý các xự kiện khi khởi tạo component
   * Author: linhpv (12/08/2022)
   */
  created() {
    // Load các dữ liệu cần thiết
    this.loadAllMenuGroup();
    this.loadAllUnit();
    this.loadAllKitchen();
    this.loadAllMaterial();
    // Set món ăn ban đầu
    this.singleDish = JSON.parse(JSON.stringify(this.selectedDish));
    // Nếu thêm thì set mặc nguyên vật liệu
    if (this.formMode === enums.formMode.Add) {
      // Nếu đã đã có món ăn được chọn => nhân bản
      if (this.singleDish.DishID) {
        // Lấy nguyên vật liệu của món ăn được nhân bản
        this.loadDishMaterial(this.singleDish.DishID);
        // Lấy mã món ăn mới
        this.getNewCode(this.singleDish.DishName);
      } else {
        // Ngược lại là thêm mới
        // Set mặc định của định lượng nguyên vật liệu
        this.dishMaterial.push(
          JSON.parse(JSON.stringify(this.defaultMaterial))
        );
      }
    } else if (this.formMode === enums.formMode.Edit) {
      // Nếu sửa thì gọi api lấy các nguyên vật liệu
      this.loadDishMaterial(this.singleDish.DishID);
    }
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