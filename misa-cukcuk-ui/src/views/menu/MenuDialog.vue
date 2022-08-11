<template>
  <div class="dialog-bg">
    <div class="dialog-form">
      <div class="mgf-inner">
        <div class="popup-dish-header">
          <div>CUKCUK - Quản lý nhà hàng</div>
        </div>
        <div class="dialog-body">
          <base-icon iconName="question-32" />
          <div class="dialog-message">
            Bạn có chắc chắn muốn xóa món &lt;&lt;{{ selectedDish.DishCode }} -
            {{ selectedDish.DishName }}&gt;&gt; không?
          </div>
        </div>
        <div class="dialog-footer">
          <base-button content="Có" @click="confirmDetele" />
          <base-button content="Không" @click="toggleDialog" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";

export default {
  computed: mapState({
    selectedDish: (state) => state.dish.selectedDish,
  }),
  methods: {
    ...mapActions(["toggleDialog", "deleteDish"]),
    /**
     * Xác nhận xóa món ăn
     * Author: linhpv (11/08/2022)
     */
    confirmDetele() {
      this.toggleDialog();
      this.deleteDish(this.selectedDish.DishID);
    },
  },
};
</script>

<style scoped>
@import url(@/css/view/menu/menudialog.css);
</style>