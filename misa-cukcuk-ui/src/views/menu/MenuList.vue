<template>
  <div class="menu-list">
    <!-- Tiêu đề -->
    <div class="menu-list-title">Thực đơn</div>
    <!-- Thanh công cụ -->
    <div class="toolbar">
      <div class="toolbar-button">
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
      <div class="toolbar-button">
        <div class="toolbar-button-icon">
          <base-icon iconName="edit" />
        </div>
        <div class="toolbar-button-text">Sửa</div>
      </div>
      <div class="toolbar-button">
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
    <!-- <dish-detail /> -->
  </div>
</template>

<script>
import MenuTable from "./MenuTable.vue";
import DishDetail from "./DishDetail.vue";
import MenuPagination from "./MenuPagination.vue";
export default {
  components: { MenuTable, DishDetail, MenuPagination },
};
</script>

<style scoped>
@import url(@/css/view/menu/menulist.css);
</style>
