<template>
  <div class="menu-list">
    <!-- Tiêu đề -->
    <div class="menu-list-title">Thực đơn</div>
    <!-- Thanh công cụ -->
    <div class="toolbar">
      <div class="toolbar-button" @click="handleAddDish">
        <div class="toolbar-button-icon">
          <base-icon iconName="insert" />
        </div>
        <div class="toolbar-button-text">Thêm</div>
      </div>
      <div class="toolbar-button">
        <div class="toolbar-button-icon">
          <base-icon iconName="duplicate" />
        </div>
        <div class="toolbar-button-text">Nhân bản</div>
      </div>
      <div class="toolbar-button" @click="toggleDishPopup">
        <div class="toolbar-button-icon">
          <base-icon iconName="edit" />
        </div>
        <div class="toolbar-button-text">Sửa</div>
      </div>
      <div class="toolbar-button" @click="toggleDialog">
        <div class="toolbar-button-icon">
          <base-icon iconName="delete" />
        </div>
        <div class="toolbar-button-text">Xóa</div>
      </div>
      <div class="separator-hor"></div>
      <div class="toolbar-button">
        <div class="toolbar-button-icon">
          <base-icon iconName="reload" />
        </div>
        <div class="toolbar-button-text">Nạp</div>
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
    <!-- Dialog -->
    <menu-dialog v-if="isShowDialog" />
  </div>
</template>

<script>
import MenuTable from "./MenuTable.vue";
import DishDetail from "./DishDetail.vue";
import MenuPagination from "./MenuPagination.vue";
import MenuGroupForm from "./MenuGroupForm.vue";
import UnitForm from "./UnitForm.vue";
import MenuDialog from "./MenuDialog.vue";
import { mapActions, mapState } from "vuex";

export default {
  components: {
    MenuTable,
    DishDetail,
    MenuPagination,
    MenuGroupForm,
    UnitForm,
    MenuDialog,
  },
  computed: mapState({
    isShowDialog: (state) => state.app.isShowDialog,
    isShowDishPopup: (state) => state.dish.isShowDishPopup,
    isShowMGPopup: (state) => state.dish.isShowMGPopup,
    isShowUnitPopup: (state) => state.dish.isShowUnitPopup,
  }),
  methods: {
    ...mapActions(["toggleDishPopup", "toggleDialog", "selectDish"]),
    /**
     * Người dùng ấn thêm
     * Author: linhpv (12/08/2022)
     */
    handleAddDish() {
      this.selectDish({});
      this.toggleDishPopup();
    },
  },
};
</script>

<style scoped>
@import url(@/css/view/menu/menulist.css);
</style>
