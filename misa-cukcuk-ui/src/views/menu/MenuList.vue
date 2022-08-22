<template>
  <div class="menu-list">
    <!-- Tiêu đề -->
    <div class="menu-list-title">{{ contentTitle.menu }}</div>
    <!-- Thanh công cụ -->
    <div class="toolbar">
      <div class="toolbar-button" @click="handleAddDish">
        <div class="toolbar-button-icon">
          <base-icon iconName="insert" />
        </div>
        <div class="toolbar-button-text">{{ toolbarContent.add }}</div>
      </div>
      <div class="toolbar-button" @click="handleReplicateDish">
        <div class="toolbar-button-icon">
          <base-icon iconName="duplicate" />
        </div>
        <div class="toolbar-button-text">{{ toolbarContent.copy }}</div>
      </div>
      <div class="toolbar-button" @click="handleEditDish">
        <div class="toolbar-button-icon">
          <base-icon iconName="edit" />
        </div>
        <div class="toolbar-button-text">{{ toolbarContent.edit }}</div>
      </div>
      <div class="toolbar-button" @click="handleDeleteDish">
        <div class="toolbar-button-icon">
          <base-icon iconName="delete" />
        </div>
        <div class="toolbar-button-text">{{ toolbarContent.delete }}</div>
      </div>
      <div class="separator-hor"></div>
      <div class="toolbar-button" @click="handleReload">
        <div class="toolbar-button-icon">
          <base-icon iconName="reload" />
        </div>
        <div class="toolbar-button-text">{{ toolbarContent.reload }}</div>
      </div>
    </div>
    <!-- Bảng -->
    <div class="menu-table">
      <menu-table />
    </div>
    <!-- Phân trang -->
    <menu-pagination />
    <!-- Popup -->
    <dish-detail v-if="isShowDishPopup" />
    <menu-group-form v-if="isShowMGPopup" />
    <unit-form v-if="isShowUnitPopup" />
    <material-form v-if="isShowMaterialPopup" />
    <kitchen-form v-if="isShowKitchenPopup" />
    <!-- Dialog confirm xóa -->
    <base-dialog
      v-if="isShowConfirmDialog"
      :message="dialogMsg"
      :rightButton="[
        { content: 'Có', type: 'confirm' },
        { content: 'Không', type: 'no-confirm' },
      ]"
      @confirm="handleConfirmDelete"
      @no-confirm="() => (this.isShowConfirmDialog = false)"
    />
    <!-- Dialog thông báo lỗi từ BE chung cho tât cả các component -->
    <base-dialog
      v-if="isShowDialog"
      type="warning"
      :message="dialogContent"
      :rightButton="[{ content: 'Đông ý', type: 'confirm' }]"
      @confirm="toggleDialog"
    />
  </div>
</template>

<script>
import MenuTable from "./MenuTable.vue";
import DishDetail from "./DishDetail.vue";
import MenuPagination from "./MenuPagination.vue";
import MenuGroupForm from "./MenuGroupForm.vue";
import UnitForm from "./UnitForm.vue";
import { mapActions, mapState } from "vuex";
import enums from "@/enums";
import resources from "@/resources";
import MaterialForm from "./MaterialForm.vue";
import KitchenForm from "./KitchenForm.vue";

export default {
  components: {
    MenuTable,
    DishDetail,
    MenuPagination,
    MenuGroupForm,
    UnitForm,
    MaterialForm,
    KitchenForm,
  },
  data() {
    return {
      isShowConfirmDialog: false,
      dialogMsg: "",
      toolbarContent: null,
      contentTitle: null,
    };
  },
  computed: mapState({
    langCode: (state) => state.app.langCode,
    isShowDialog: (state) => state.app.isShowDialog,
    dialogContent: (state) => state.app.dialogContent,
    isShowDishPopup: (state) => state.dish.isShowDishPopup,
    isShowMGPopup: (state) => state.dish.isShowMGPopup,
    isShowUnitPopup: (state) => state.dish.isShowUnitPopup,
    isShowMaterialPopup: (state) => state.dish.isShowMaterialPopup,
    isShowKitchenPopup: (state) => state.dish.isShowKitchenPopup,
    selectedDish: (state) => state.dish.selectedDish,
  }),
  methods: {
    ...mapActions([
      "toggleDishPopup",
      "toggleDialog",
      "selectDish",
      "setFormMode",
      "updatePageIndex",
      "loadDishsByPaging",
      "deleteDish",
    ]),
    /**
     * Người dùng xác nhận xóa món ăn
     * Author: linhpv (19/08/2022)
     */
    handleConfirmDelete() {
      this.isShowConfirmDialog = false;
      this.deleteDish(this.selectedDish.DishID);
    },
    /**
     * Người dùng ấn xóa
     * Author: linhpv (19/08/2022)
     */
    handleDeleteDish() {
      this.isShowConfirmDialog = true;
      this.dialogMsg = resources[`${this.langCode}_Confirm_Delete`](
        this.selectedDish.DishCode,
        this.selectedDish.DishName
      );
    },
    /**
     * Người dùng ấn nhân bản
     * Author: linhpv (19/08/2022)
     */
    handleReplicateDish() {
      this.setFormMode(enums.formMode.Add);
      this.toggleDishPopup();
    },
    /**
     * Người dùng nạp lại dữ liệu
     * Author: linhpv (18/08/2022)
     */
    handleReload() {
      // Về trang đầu
      this.updatePageIndex(1);
      // Load lại dữ liệu
      this.loadDishsByPaging();
    },
    /**
     * Người dùng ấn sửa
     * Author: linhpv (15/08/2022)
     */
    handleEditDish() {
      this.setFormMode(enums.formMode.Edit);
      this.toggleDishPopup();
    },
    /**
     * Người dùng ấn thêm
     * Author: linhpv (12/08/2022)
     */
    handleAddDish() {
      this.selectDish({
        ShowOnMenu: enums.yesNo.Yes,
        SemiFinishedProduct: enums.yesNo.No,
      });
      this.setFormMode(enums.formMode.Add);
      this.toggleDishPopup();
    },
  },
  created() {
    this.contentTitle = resources[`${this.langCode}_Menu_Item`];
    this.toolbarContent = resources[`${this.langCode}_Toolbar`];
  },
};
</script>

<style scoped>
@import url(@/css/view/menu/menulist.css);
</style>
