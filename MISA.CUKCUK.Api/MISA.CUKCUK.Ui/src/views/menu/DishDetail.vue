<template>
  <div class="popup-dish-bg">
    <div class="popup-dish">
      <div class="popup-dish-inner">
        <div class="popup-dish-header">
          <div>
            {{ formTitle }}
          </div>
          <div @click="handleClosePopup">
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
                      (val, tab) => this.setValueCombobox('MenuGroup', val)
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
                    @change="
                      (val, tab) => this.checkComboboxRequired('Unit', val)
                    "
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
                    :addIcon="true"
                    :value="singleDish.KitchenID"
                    @change="
                      (val, tab) => this.setValueCombobox('Kitchen', val)
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
                        v-if="!singleDish.LinkImage"
                        ref="imagePreview"
                        src="@/assets/images/ImageHandler.png"
                        width="160"
                        height="120"
                      />
                      <img
                        v-if="singleDish.LinkImage"
                        ref="imagePreview"
                        :src="`${serverLink}${singleDish.LinkImage}`"
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
                      <label class="pdi-label-icon" @click="handleRevokeImage">
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
                      @contextmenu="handleRightClick(index, $event)"
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
              <base-button
                content="Cất"
                icon="save"
                @click="handleStore(enums.storeMode.Store)"
              />
              <base-button
                content="Cất & thêm"
                icon="save-add"
                @click="handleStore(enums.storeMode.StoreAndAdd)"
              />
              <base-button
                content="Hủy bỏ"
                icon="cancel"
                @click="toggleDishPopup"
              />
            </div>
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
        this.handleStore(enums.storeMode.Store);
      }
    "
    @no-confirm="toggleDishPopup"
    @cancel="() => (this.isShowConfirmDialog = false)"
  />
  <!-- Option menu trên table -->
  <div
    v-if="isShowOptionMenu"
    class="option-menu"
    :style="{ position: 'fixed', top: `${menuTop}px`, left: `${menuLeft}px` }"
    v-click-outside="() => (this.isShowOptionMenu = false)"
  >
    <div
      class="option-menu-item"
      @click="
        () => {
          this.isShowOptionMenu = false;
          this.addRowMaterial();
        }
      "
    >
      <div class="option-menu-icon">
        <base-icon iconName="insert" />
      </div>
      <div class="toolbar-button-text">Thêm dòng</div>
    </div>
    <div
      class="option-menu-item"
      @click="
        () => {
          this.isShowOptionMenu = false;
          this.handleDeleteRow();
        }
      "
    >
      <div class="option-menu-icon">
        <base-icon iconName="delete" />
      </div>
      <div class="toolbar-button-text">Xóa dòng</div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import resources from "@/resources";
import enums from "@/enums";
import { constants } from "@/config";
import { mapActions, mapState } from "vuex";
import { objectEqual } from "@/utils";

export default {
  data() {
    return {
      enums: enums,
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
      deletedDMList: [],
      isShowConfirmDialog: false,
      dialogMsg: "",
      serverLink: "",
      imageFile: null,
      menuTop: 0,
      menuLeft: 0,
      isShowOptionMenu: false,
      defaultImage: null,
    };
  },
  computed: mapState({
    langCode: (state) => state.app.langCode,
    menuGroups: (state) => state.menuGroup.menuGroups,
    units: (state) => state.unit.units,
    kitchens: (state) => state.kitchen.kitchens,
    materials: (state) => state.material.materials,
    selectedDish: (state) => state.dish.selectedDish,
    formMode: (state) => state.dish.formMode,
  }),
  watch: {
    /**
     * Bắt sự kiện thay đổi món ăn được chọn
     * @param {*} newVal món ăn mới
     * Created by: linhpv (23/08/2022)
     */
    selectedDish(newVal) {
      // Quay lại tab 1
      this.popupTab = 1;
      // Gán lại món ăn vào form
      this.singleDish = JSON.parse(JSON.stringify(newVal));
      // Gán nguyên vật liệu mặc định
      this.dishMaterial = [JSON.parse(JSON.stringify(this.defaultMaterial))];
    },
  },
  methods: {
    ...mapActions([
      "toggleDishPopup",
      "loadAllMenuGroup",
      "loadAllUnit",
      "loadAllKitchen",
      "insertDish",
      "loadAllMaterial",
      "updateDish",
      "changeDialogContent",
      "toggleDialog",
      "setStoreMode",
    ]),
    /**
     * Xóa ảnh đã chọn
     * Author: linhpv (26/08/2022)
     */
    handleRevokeImage() {
      // Bỏ preview
      let imgPreview = this.$refs.imagePreview;
      URL.revokeObjectURL(imgPreview.src);
      imgPreview.src = this.defaultImage;
      // Set trường image trong món ăn thành null
      this.singleDish.LinkImage = null;
      // Set data ảnh = null
      this.imageFile = null;
    },
    /**
     * Xử lý sự kiện click chuột phải
     * @param {*} i index
     * @param {*} e event
     * Created by: linhpv (25/08/2022)
     */
    handleRightClick(i, e) {
      // Ngăn sự kiện mặc định
      e.preventDefault();
      // Chọn dòng đang focus
      this.dishMaterialFocus = i;
      // Lấy vị trí option menu
      this.menuLeft = e.pageX;
      this.menuTop = e.pageY;
      this.isShowOptionMenu = true;
    },
    /**
     * Người dùng ấn đóng popup
     * Author: linhpv (19/08/2022)
     */
    handleClosePopup() {
      if (objectEqual(this.singleDish, this.selectedDish)) {
        this.toggleDishPopup();
      } else {
        this.dialogMsg = resources[`${this.langCode}_Dialog_Msg`].dataChanged;
        this.isShowConfirmDialog = true;
      }
    },

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
        material.MaterialAmount = 1;
        material.TotalPrice = material.MaterialPurchasePrice;
        return;
      }
      // Nếu người dùng không nhập giá vốn => set giá vốn = 0
      if (!material.MaterialPurchasePrice) {
        material.MaterialPurchasePrice = 0;
        material.TotalPrice = 0;
        return;
      }
      // Tính tổng tiền nguyên vật liệu
      material.TotalPrice =
        parseFloat(material.MaterialAmount.toString().replaceAll(".", "")) *
        parseFloat(
          material.MaterialPurchasePrice.toString().replaceAll(".", "")
        );
      // Tính giá vốn món ăn
      this.singleDish.PurchasePrice = 0;
      this.dishMaterial.forEach((dm) => {
        this.singleDish.PurchasePrice += dm.TotalPrice;
      });
    },
    /**
     * Xóa 1 dòng nguyên vật liệu
     * Author: linhpv (16/08/2022)
     */
    handleDeleteRow() {
      // Nếu số lượng khác không thì xóa
      if (this.dishMaterial.length > 0) {
        // Nếu xóa nguyên vật liệu đã thêm vào bảng thì lấy danh sách
        if (this.dishMaterial[this.dishMaterialFocus].DishMaterialID) {
          this.deletedDMList.unshift(
            this.dishMaterial[this.dishMaterialFocus].DishMaterialID
          );
        }
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
    handleChangeMaterial(material, table) {
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
    async handleStore(storeMode) {
      this.focusElm = "";
      var valid = this.validateData();
      if (valid) {
        // Format lại các định dạng giá tiền
        this.formatData();
        // Xử lý định lượng nguyên vật liệu
        this.handleDishMaterial();
        // Set ảnh
        if (this.imageFile) {
          await this.handleUploadImage();
        }
        // Set store mode
        this.setStoreMode(storeMode);
        // Check formMode
        if (this.formMode == enums.formMode.Add) {
          this.insertDish(this.singleDish);
        } else if (this.formMode == enums.formMode.Edit) {
          this.updateDish(this.singleDish);
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
      // Thêm danh sách các nguyên vật liệu đã bị xóa
      this.singleDish.DeletedDM = this.deletedDMList;
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
        this.errorList.DishName =
          resources[`${this.langCode}_Error_Msg`].emptyName;
        valid = false;
      }
      // 2. Mã món ăn
      if (!this.singleDish.DishCode) {
        if (!this.focusElm) {
          this.focusElm = "DishCode";
        }
        this.errorList.DishCode =
          resources[`${this.langCode}_Error_Msg`].emptyCode;
        valid = false;
      }
      // 3. Đơn vị tính
      if (!this.singleDish.UnitID) {
        if (!this.focusElm) {
          this.focusElm = "UnitID";
        }
        this.errorList.UnitID =
          resources[`${this.langCode}_Error_Msg`].emptyUnit;
        valid = false;
      }
      // 4. Giá bán
      if (!this.singleDish.Price) {
        if (!this.focusElm) {
          this.focusElm = "Price";
        }
        this.errorList.Price =
          resources[`${this.langCode}_Error_Msg`].emptyPrice;
        valid = false;
      }
      // 5. Check trùng nguyên vật liệu
      let findDuplicates = (arr) =>
        arr.filter(
          (item, index) =>
            arr.findIndex((x) => x.MaterialID === item.MaterialID) != index
        );
      if (findDuplicates(this.dishMaterial).length > 0) {
        valid = false;
        this.changeDialogContent(
          resources[`${this.langCode}_Error_Msg`].duplicateMaterial
        );
        this.toggleDialog();
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
          resources[`${this.langCode}_Error_Msg`].required;
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
          resources[`${this.langCode}_Error_Msg`].required;
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
     * Xử lý upload ảnh
     * Author: linhpv (22/08/2022)
     */
    async handleUploadImage() {
      // Upload ảnh và lấy link
      let formData = new FormData();
      formData.append("image", this.imageFile);

      const res = await axios.post(
        `${constants.API_URL}/api/${constants.API_VERSION}/Dish/Image`,
        formData,
        {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        }
      );

      if (res.data.Success) {
        this.singleDish.LinkImage = res.data.Data;
      } else {
        this.changeDialogContent(
          resources[`${this.langCode}_Error_Msg`].capacity
        );
        this.toggleDialog();
      }
    },

    /**
     * Preview ảnh
     * @param {*} e
     * Author: linhpv (10/08/2022)
     */
    handleChangeImage(e) {
      const file = e.target.files[0];

      // Kiểm tra dung lượng ảnh có hợp lệ hay không
      if (file.size > 5 * 1024 * 1024) {
        this.changeDialogContent(
          resources[`${this.langCode}_Error_Msg`].capacity
        );
        this.toggleDialog();
      } else {
        let imgPreview = this.$refs.imagePreview;
        this.defaultImage = imgPreview.src;
        URL.revokeObjectURL(imgPreview.src);
        imgPreview.src = URL.createObjectURL(file);

        this.imageFile = file;
      }
      e.target.value = null;
    },
  },
  /**
   * Xử lý các xự kiện khi khởi tạo component
   * Author: linhpv (12/08/2022)
   */
  async created() {
    // Load các dữ liệu cần thiết
    await this.loadAllMenuGroup();
    await this.loadAllUnit();
    await this.loadAllKitchen();
    await this.loadAllMaterial();
    // Set món ăn ban đầu
    this.singleDish = JSON.parse(JSON.stringify(this.selectedDish));
    // Nếu thêm thì set mặc nguyên vật liệu
    if (this.formMode === enums.formMode.Add) {
      // Nếu đã đã có món ăn được chọn => nhân bản
      if (this.singleDish.DishID) {
        // Lấy nguyên vật liệu của món ăn được nhân bản
        this.loadDishMaterial(this.singleDish.DishID);
        // Lấy mã món ăn mới
        await this.getNewCode(this.singleDish.DishName);
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
    // Lấy link server
    this.serverLink = constants.API_URL;
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
@import url(@/css/view/menu/dishdetail.css);
</style>