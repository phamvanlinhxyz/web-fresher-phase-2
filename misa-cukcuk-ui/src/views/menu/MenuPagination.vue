<template>
  <div class="menu-pagination">
    <div class="menu-pagination-left">
      <div class="pagination-icon">
        <base-icon iconName="page-first" />
      </div>
      <div class="pagination-icon">
        <base-icon iconName="page-prev" />
      </div>
      <div class="separator-hor"></div>
      <div class="pagination-icon">Trang</div>
      <base-input v-model="pageNumber" />
      <div class="pagination-icon">trên {{ totalPage }}</div>
      <div class="separator-hor"></div>
      <div class="pagination-icon">
        <base-icon iconName="page-next" />
      </div>
      <div class="pagination-icon">
        <base-icon iconName="page-last" />
      </div>
      <div class="separator-hor"></div>
      <div class="pagination-icon">
        <base-icon iconName="refresh" />
      </div>
      <div class="separator-hor"></div>
    </div>
    <div class="menu-pagination-right">
      Hiển thị {{ (pageIndex - 1) * pageSize + 1 }} -
      {{ pageIndex * pageSize }} trên {{ totalRecord }} kết quả
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  data() {
    return {
      pageNumber: 1,
    };
  },
  computed: mapState({
    pageIndex: (state) => state.dish.pageIndex,
    pageSize: (state) => state.dish.pageSize,
    totalPage: (state) => state.dish.totalPage,
    totalRecord: (state) => state.dish.totalRecord,
  }),
};
</script>

<style scoped>
@import url(@/css/view/menu/menupagination.css);
</style>