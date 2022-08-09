<template>
  <table class="table">
    <thead class="table-header">
      <tr>
        <td style="min-width: 159px">
          <div class="table-column">Loại món</div>
          <div class="table-filter">
            <input class="input" value="Món ăn" />
          </div>
        </td>
        <td style="min-width: 179px">
          <div class="table-column">Mã món</div>
          <div class="table-filter">
            <div class="filter-type">*</div>
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column">Tên món</div>
          <div class="table-filter">
            <div class="filter-type">*</div>
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column">Nhóm thực đơn</div>
          <div class="table-filter">
            <div class="filter-type">*</div>
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 89px">
          <div class="table-column">Đơn vị tính</div>
          <div class="table-filter">
            <div class="filter-type">*</div>
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 119px">
          <div class="table-column">Giá bán</div>
          <div class="table-filter">
            <div class="filter-type">≤</div>
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 150px">
          <div class="table-column">Thay đổi theo thời giá</div>
          <div class="table-filter">
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column">Điều chỉnh tự do</div>
          <div class="table-filter">
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 139px">
          <div class="table-column">Định lượng NVL</div>
          <div class="table-filter">
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column">Hiển thị trên thực đơn</div>
          <div class="table-filter">
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 89px">
          <div class="table-column">Ngừng bán</div>
          <div class="table-filter">
            <input class="input" value="Không" />
          </div>
        </td>
      </tr>
    </thead>
    <tbody class="table-body" v-if="!isLoadingDish">
      <tr v-for="dish of dishs" key="dish.dishID">
        <td>Món ăn</td>
        <td>{{ dish.DishCode }}</td>
        <td>{{ dish.DishName }}</td>
        <td>{{ dish.MenuGroupName }}</td>
        <td>{{ dish.UnitName }}</td>
        <td align="right">{{ handlePrice(dish.Price) }}</td>
        <td>
          <base-icon iconName="checkbox-empty" />
        </td>
        <td><base-icon iconName="checkbox-empty" /></td>
        <td>{{ handleMaterialQuantified(dish.MaterialQuantified) }}</td>
        <td>
          <base-icon iconName="checkbox-empty" v-if="dish.ShowOnMenu === 0" />
          <base-icon iconName="checkbox-tick" v-if="dish.ShowOnMenu === 1" />
        </td>
        <td><base-icon iconName="checkbox-empty" /></td>
      </tr>
    </tbody>
    <div class="table-loading" v-if="isLoadingDish">
      <base-loading content="Đang lấy dữ liệu" />
    </div>
  </table>
</template>

<script>
import { mapActions, mapState } from "vuex";

export default {
  computed: mapState({
    dishs: (state) => state.dish.dishs,
    isLoadingDish: (state) => state.dish.isLoadingDish,
  }),
  methods: {
    ...mapActions(["loadDishsByPaging"]),
    /**
     * Xử lý giá bán
     * @param {string} val
     * Author: linhpv (09/08/2022)
     */
    handlePrice(val) {
      if (!val) val = 0;
      return val.toLocaleString("vi-VN", {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
      });
    },
    /**
     * Xử lý định lượng nguyên vật liệu
     * @param {string} val
     * Author: linhpv (09/08/2022)
     */
    handleMaterialQuantified(val) {
      switch (val) {
        case 0:
          return "Chưa thiết lập";
        case 1:
          return "Đẫ thiết lập";
        default:
          return "";
      }
    },
  },
  created() {
    this.loadDishsByPaging();
  },
};
</script>

<style>
@import url(@/css/view/menu/menutable.css);
</style>